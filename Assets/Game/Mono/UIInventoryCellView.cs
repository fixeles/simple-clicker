using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mono
{
    public class UIInventoryCellView : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text count;

        public Sprite Icon
        {
            set => image.sprite = value;
        }

        public string Count
        {
            set => count.text = value;
        }
    }
}