using System.Threading;
using Common;
using Config;
using Cysharp.Threading.Tasks;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace ECS
{
    public class IncomeBufferSystem : IEcsInitSystem
    {
        private readonly CancellationToken _cancellationToken;
        private readonly EcsCustomInject<Inventory> _inventory;
        private readonly EcsCustomInject<RuntimeData> _runtimeData;
        private readonly EcsCustomInject<StaticData> _staticData;

        public IncomeBufferSystem(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
        }

        public void Init(IEcsSystems systems)
        {
            _inventory.Value.UpdateEvent += UpdateBuffer;
        }

        private void UpdateBuffer(string itemId, Inventory.CountChangeData data)
        {
            //todo: add subscriptions by key 
            if (itemId is not "soft")
                return;

            RegisterIncomeAsync(data.Difference).Forget();
        }

        private async UniTaskVoid RegisterIncomeAsync(double count)
        {
            _runtimeData.Value.IncomeBuffer.Enqueue(count);
            await UniTask.WaitForSeconds(_staticData.Value.IncomeData.IncomeBufferLifetimeSeconds, cancellationToken: _cancellationToken).SuppressCancellationThrow();
            if (_cancellationToken.IsCancellationRequested)
                return;

            _runtimeData.Value.IncomeBuffer.Dequeue();
        }
    }
}