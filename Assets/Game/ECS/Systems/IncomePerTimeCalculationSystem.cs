using System.Linq;
using System.Threading;
using Common;
using Config;
using Cysharp.Threading.Tasks;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS
{
    public class IncomePerTimeCalculationSystem : IEcsInitSystem
    {
        private readonly CancellationToken _cancellationToken;
        private readonly EcsCustomInject<Inventory> _inventory;
        private readonly EcsCustomInject<RuntimeData> _runtimeData;
        private readonly EcsCustomInject<StaticData> _staticData;

        public IncomePerTimeCalculationSystem(CancellationToken cancellationToken)
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
            Debug.Log(_runtimeData.Value.IncomeBuffer.Sum());
            await UniTask.WaitForSeconds(_staticData.Value.IncomeData.IncomeBufferLifetimeSeconds, cancellationToken: _cancellationToken).SuppressCancellationThrow();
            if (_cancellationToken.IsCancellationRequested)
                return;

            Debug.Log(_runtimeData.Value.IncomeBuffer.Sum());
            _runtimeData.Value.IncomeBuffer.Dequeue();
        }
    }
}