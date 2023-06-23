using UnityEngine;
using UnityEngine.UI;

namespace Viewer {
    /// <summary>
    /// Класс, отвечающий за отображение изображения в просмотрщике.
    /// </summary>
    public class RenderViewerImage : MonoBehaviour {
        [SerializeField] private Image image;
        private Vector3 _savedPosition;
        private Quaternion _savedRotation;
        private Vector3 _savedScale;
        private Vector2 _savedSizeDelta;
        private int _numberImage;

        private void Start() {
            RectTransform rectTransform = GetComponent<RectTransform>();

            // Сохраняем параметры компонента RectTransform
            _savedPosition = rectTransform.position;
            _savedSizeDelta = rectTransform.sizeDelta;
            _savedScale = rectTransform.localScale;
            _savedRotation = rectTransform.localRotation;

            Texture2D texture = StateGalleryToViewer.PickedTexture2D;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

            // Присваиваем спрайт компоненту Image
            image.sprite = sprite;
            image.color = Color.white;
            image.preserveAspect = true;
        }

        /// <summary>
        /// Сбрасывает изображение в исходное состояние.
        /// </summary>
        public void ResetImage() {
            RectTransform rectTransform = GetComponent<RectTransform>();

            // Восстанавливаем сохраненные параметры компонента RectTransform
            rectTransform.position = _savedPosition;
            rectTransform.sizeDelta = _savedSizeDelta;
            rectTransform.localScale = _savedScale;
            rectTransform.localRotation = _savedRotation;
        }
    }
}