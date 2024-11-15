using System;
using FPS;
using UnityEngine;

namespace Config
{
    [Serializable]
    public class PrefabsData
    {
        [SerializeField] private SerializableDictionary<string, Sprite> inventoryIcons;

        public Sprite GetInventoryItemIcon(string id) => !inventoryIcons.ContainsKey(id) ? null : inventoryIcons[id];
    }
}