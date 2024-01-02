using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;
using Leopotam.Ecs.UnityIntegration;

namespace Noobik_Thaumcraft
{
    public sealed class Startup : MonoBehaviour
    {
        public EcsWorld World;
        private EcsSystems _systems;
        private EcsSystems _fixedSystems;
        
        public SceneData SceneData;
        public Configuration Configuration;
        
        public UI UI;
        
        [HideInInspector]
        public GameData GameData;

        public new static Startup Instantiate;

        [Header("Мусор")]
        [SerializeField]
        private SaveLoadService _saveLoadService;

        private void Start()
        {
            Instantiate = this;
            
            World = new EcsWorld();

#if UNITY_EDITOR
            EcsWorldObserver.Create(World);
#endif  
            
            GameData = new GameData();

            _systems = new EcsSystems(World);
            _fixedSystems = new EcsSystems(World);

            _systems.ConvertScene();
            //_fixedSystems.ConvertScene();

            AddSystems();
            AddOneFrames();
            AddInjections();

            _systems.Init();
            _fixedSystems.Init();
            
#if UNITY_EDITOR
            EcsSystemsObserver.Create(_systems);
            EcsSystemsObserver.Create(_fixedSystems);
#endif
        }

        private void Update()
        {
            _systems.Run();
        }

        private void FixedUpdate()
        {
            _fixedSystems.Run();
        }
        
        private void AddSystems()
        {
            _systems
                .Add(new InitializeAllEntityBehaviourSystem())      //Пробрассываем ссылки Entity на сцену
                .Add(new InitializeGameDataSystem())                //Инициализируем локальные данные
                .Add(new InputPlayerSystem())                       //Обрыбатываем ввод с клавиатуры
                .Add(new InputJoystickSystem())                     //Обрыбатываем ввод с джостика
                .Add(new MovementSystem())                          //Двигаем игрока
                .Add(new RotateAtMoveSystem())                      //Выставляем поворот
                .Add(new RotateSystem())                            //Поворачиваем игрока
                .Add(new SelectTargetSystem())                      //Выбираем ближайшую цель
                .Add(new ArrowRotateAtTargetSystem())               //поворачиваем к цели стрелку
                .Add(new ArrowHideAtTargetDistanceSystem())         //Скрываем стрелку если находимся сшлищком близко к цели
                .Add(new MiningSystem())                            //Ждём таймера и превращаем блок в айтем
                
                .Add(new PickUpItemSystem())                        //Выбираем предмет вешаем таймер и подбираем в рюкзак
                .Add(new TrashDropItemSystem())                      //Кидаем предмет в мусорку
                
                .Add(new MachineDropItemSystem())                   //Кидаем предмет в машину
                .Add(new MachineCreateSystem())                     //Создаём предмет если есть ресурсы
                .Add(new PickUpMachineResultSystem())               //Забераем предмет из машины

                .Add(new ItemStartMoveSystem())                      //Создаём анимацию движения предмета
                .Add(new ItemMoveCompleteSystem())                   //Кладём предмет куда-то и что-то с ним делаем
                
                //.Add(new TimerNotDropSystem())                           //Убираем таймер запрещающий кидать вещь в машину
                
                ;
            
            _fixedSystems
                .Add(new SelectMiningTargetBlockSystem()) //Выбираем блок для добычи
                ;
        }

        private void AddInjections()
        {
            _systems
                .Inject(SceneData)
                .Inject(Configuration)
                .Inject(GameData)
                //Удалить
                .Inject(_saveLoadService);
        }

        private void AddOneFrames()
        {
            _systems
                .OneFrame<EventMove>();
        }

        private void OnDestroy()
        {
            if (_systems == null)
                return;
        
            _systems.Destroy();
            _fixedSystems.Destroy();
            _systems = null;
            _fixedSystems = null;
            World.Destroy();
            World = null;
        }
    }
}