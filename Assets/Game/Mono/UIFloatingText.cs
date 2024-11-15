using Cysharp.Threading.Tasks;
using FPS;
using FPS.Pool;
using TMPro;
using UnityEngine;

namespace Mono
{
    public class UIFloatingText : MonoBehaviour
    {
        private static Camera _mainCamera;

        [SerializeField, Get] private TextMeshProUGUI content;
        [SerializeField, Get] private CanvasGroup canvasGroup;
        [SerializeField] private float upwardSpeed = 100;
        [SerializeField] private float clickOffset = 50;
        [SerializeField, Min(0.01f)] private float fadeTime = 1;
        [SerializeField, Get] private Transform cachedTransform;


        public void Show(Transform parent, string text)
        {
            cachedTransform.SetParent(parent, false);
            content.text = text;
            canvasGroup.alpha = 1;
            FadeAfterClickAsync().Forget();
        }

        private async UniTaskVoid FadeAfterClickAsync()
        {
            var uiPosition = Input.mousePosition;
            uiPosition.y += clickOffset;
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / fadeTime;
                uiPosition.y += Time.deltaTime * upwardSpeed;
                cachedTransform.position = uiPosition;
                await UniTask.Yield();
            }

            FluffyPool.Return(this);
        }

        private void Awake()
        {
            _mainCamera ??= Camera.main;
        }
    }
}