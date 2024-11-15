using System.Threading;
using Common;
using Config;
using FPS.Pool;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace ECS
{
    public class AppInitSystem : IEcsInitSystem
    {
        private readonly CancellationToken _cancellationToken;
        private readonly EcsCustomInject<Inventory> _inventory;
        private readonly EcsCustomInject<StaticData> _staticData;

        public AppInitSystem(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
        }

        public void Init(IEcsSystems systems)
        {
            FluffyPool.Init(_cancellationToken);
            _inventory.Value.AddItem("energy", _staticData.Value.BaseEnergy);
        }
    }
}