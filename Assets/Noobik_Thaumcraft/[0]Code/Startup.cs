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
        
        public SceneData SceneData;
        public Configuration Configuration;
        public GameData GameData;

        public static Startup Instantiate;

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
            
            _systems = new EcsSystems(World);

            _systems.ConvertScene();

            AddSystems();
            AddOneFrames();
            AddInjections();

            _systems.Init();
            
#if UNITY_EDITOR
            EcsSystemsObserver.Create (_systems);
#endif
        }

        private void Update()
        {
            _systems.Run();
        }

        private void AddSystems()
        {
            _systems
                .Add(new EntityInitializeSystem())          //Пробрассываем ссылки Entity на сцену
                .Add(new PlayerInputSystem())               //Обрыбатываем ввод с клавиатуры
                .Add(new JoystickInputSystem())             //Обрыбатываем ввод с джостика
                .Add(new MovementSystem())                  //Двигаем игрока
                .Add(new RotateAtMoveSystem())              //Выставляем поворот
                .Add(new RotateSystem())                    //Поворачиваем игрока
                .Add(new SelectTargetSystem())              //Выбираем ближайшую цель
                .Add(new ArrowRotateAtTargetSystem())       //поворачиваем к цели стрелку
                .Add(new ArrowHideAtTargetDistanceSystem()) //Скрываем стрелку если находимся сшлищком близко к цели
                .Add(new TargetBlockSelectSystem())         //Выбираем блок для добычи
                //.Add(new StartMiningSystem())               //Выбираем блок как цель для поворота и вешаем таймер
                .Add(new MiningSystem())                    //Ждём таймера и превращаем блок в айтем
                //.Add(new StopMiningToMoveSystem())          //Убираем таймер для добычи
                .Add(new PickUpItemSystem())                //Выбираем предмет вешаем таймер и подбираем в рюкзак
                .Add(new DropItemToMachineSystem())         //Кидаем предмет в машину
                .Add(new NotDropTimerSystem())              //Убираем таймер запрещающий кидать вещь в машину
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
                .OneFrame<MoveEvent>();
        }

        private void OnDestroy()
        {
            if (_systems == null)
                return;
        
            _systems.Destroy();
            _systems = null;
            World.Destroy();
            World = null;
        }
    }
}