using Common;
using Config;
using FPS;
using FPS.Pool;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Mono;

namespace ECS
{
    public class TapRewardSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<StaticData> _staticData;
        private readonly EcsCustomInject<SceneData> _sceneData;
        private readonly EcsCustomInject<Inventory> _inventory;
        private readonly EcsCustomInject<RuntimeData> _runtimeData;

        public void Init(IEcsSystems systems)
        {
            _sceneData.Value.GetMoneyButton.onClick.AddListener(TryGetMoney);
        }

        private void TryGetMoney()
        {
            if (!_inventory.Value.TrySubtractItem("energy", _staticData.Value.TapEnergyCost))
                return;

            double reward = _staticData.Value.IncomeData.CalculateClickReward(_runtimeData.Value);
            _inventory.Value.AddItem("soft", reward);
            var text = FluffyPool.Get<UIFloatingText>();
            text.Show(_sceneData.Value.FrequentCanvas, $"+{reward.ToShortString()}");
        }
    }
}