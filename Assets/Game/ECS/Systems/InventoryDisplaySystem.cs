using System.Collections.Generic;
using Common;
using Config;
using FPS;
using FPS.Pool;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Mono;

namespace ECS
{
    public class InventoryDisplaySystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<SceneData> _sceneData;
        private readonly EcsCustomInject<StaticData> _staticData;
        private readonly EcsCustomInject<Inventory> _inventory;

        private readonly Dictionary<string, UIInventoryCellView> _activeViews = new();


        public void Init(IEcsSystems systems)
        {
            _inventory.Value.UpdateEvent += UpdateCellView;
            foreach (var kvp in _inventory.Value.AllItems)
            {
                InitNewCell(kvp.Key, kvp.Value);
            }
        }

        private void InitNewCell(string id, double count)
        {
            var cell = FluffyPool.Get<UIInventoryCellView>();
            cell.transform.SetParent(_sceneData.Value.InventoryView);
            cell.Icon = _staticData.Value.PrefabsData.GetInventoryItemIcon(id);
            _activeViews.Add(id, cell);
            UpdateCellView(id, new Inventory.CountChangeData
            {
                Total = count
            });
        }

        private void UpdateCellView(string id, Inventory.CountChangeData count)
        {
            if (!_activeViews.ContainsKey(id))
                InitNewCell(id, count.Total);

            _activeViews[id].Count = count.Total.ToShortString();
        }
    }
}