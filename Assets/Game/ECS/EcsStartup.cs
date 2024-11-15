using Common;
using Config;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Mono;
using UnityEngine;

namespace ECS
{
    public class EcsStartup : MonoBehaviour
    {
        [SerializeField] private StaticData staticData;
        [SerializeField] private SceneData sceneData;

        private EcsWorld _world;
        private IEcsSystems _systems;

        private void Awake()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            var cancellationToken = destroyCancellationToken;
            _systems
                .Add(new AppInitSystem(cancellationToken))
                .Add(new AutoRewardSystem())
                .Add(new TapRewardSystem())
                .Add(new InventoryDisplaySystem())
                .Add(new AutoRewardProgressViewSystem()) //can disable progress view
                .Add(new IncomeBufferSystem(cancellationToken))
                .Inject(new Inventory(), new RuntimeData(), sceneData, staticData)
                .Init();
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            _systems?.Destroy();
            _systems = null;

            _world?.Destroy();
            _world = null;
        }
    }
}