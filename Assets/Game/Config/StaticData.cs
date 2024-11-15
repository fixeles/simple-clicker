using UnityEngine;

namespace Config
{
    [CreateAssetMenu]
    public class StaticData : ScriptableObject
    {
        [field: SerializeField] public int BaseEnergy { get; private set; }
        [field: SerializeField] public int TapEnergyCost { get; private set; }

        [field: SerializeField] public IncomeData IncomeData { get; private set; }
        [field: SerializeField] public PrefabsData PrefabsData { get; private set; }
    }
}