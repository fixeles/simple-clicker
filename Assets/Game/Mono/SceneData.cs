using UnityEngine;
using UnityEngine.UI;

namespace Mono
{
    public class SceneData : MonoBehaviour
    {
        [field: SerializeField] public Button GetMoneyButton { get; private set; }

        [field: SerializeField] public Transform FrequentCanvas { get; private set; }
        [field: SerializeField] public Transform InventoryView { get; private set; }
        [field: SerializeField] public Image AutoRewardProgressView { get; private set; }

    }
}