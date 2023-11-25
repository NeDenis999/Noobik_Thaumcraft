using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;
using Leopotam.Ecs.UnityIntegration;

namespace Noobik_Thaumcraft
{
    public sealed class EcsGameStartup : MonoBehaviour
    {
        [SerializeField]
        private CameraShaker _cameraShaker;

        [SerializeField]
        private TargetsDataManager _targetsManager;
        
        [SerializeField]
        private TargetPointsContainer _targetPointsContainer;

        [SerializeField]
        private SaveLoadService _saveLoadService;
        
        [SerializeField]
        private Joystick _joystick;
        
        private EcsWorld _world;
        private EcsSystems _systems;

        private void Start()
        {
            _world = new EcsWorld();

#if UNITY_EDITOR
            EcsWorldObserver.Create(_world);
#endif  
            
            _systems = new EcsSystems(_world);

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
                .Add(new EntityInitializeSystem())
                .Add(new PlayerInputSystem())
                .Add(new JoystickInputSystem())
                .Add(new MovementSystem())
                .Add(new RotateAtMoveSystem())
                .Add(new ArrowRotateAtTargetSystem())
                .Add(new ArrowHideAtTargetDistanceSystem())
                .Add(new TargetBlockSelectSystem())
                .Add(new StartMiningSystem())
                .Add(new MiningSystem())
                .Add(new PickUpItemSystem())
                .Add(new DropItemToMachineSystem())
                ;
        }

        private void AddInjections()
        {
            _systems.Inject(_cameraShaker);
            _systems.Inject(_targetsManager);
            _systems.Inject(_targetPointsContainer);
            _systems.Inject(_saveLoadService);
            _systems.Inject(_joystick);
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
            _world.Destroy();
            _world = null;
        }
    }
}