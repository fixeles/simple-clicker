using Common;
using Config;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS
{
    public class AutoRewardSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsCustomInject<StaticData> _staticData;
        private readonly EcsCustomInject<Inventory> _inventory;
        private readonly EcsWorldInject _world;
        private readonly EcsFilterInject<Inc<ProgressComponent>> _filter;


        public void Init(IEcsSystems systems)
        {
            InitAutoRewardsEntities();
        }

        private void InitAutoRewardsEntities()
        {
            var autoRewardEntity = _world.Value.NewEntity();
            ref var progressComponent = ref _filter.Pools.Inc1.Add(autoRewardEntity);
            progressComponent.LoopTime = _staticData.Value.IncomeData.AutoRewardFrequencySeconds;
            progressComponent.TimeLeft = progressComponent.LoopTime;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var progressComponent = ref _filter.Pools.Inc1.Get(entity);
                progressComponent.TimeLeft -= Time.deltaTime;

                if (progressComponent.TimeLeft > 0)
                    return;

                progressComponent.TimeLeft += progressComponent.LoopTime;
                _inventory.Value.AddItem("soft", _staticData.Value.IncomeData.AutoRewardCount);
            }
        }
    }
}