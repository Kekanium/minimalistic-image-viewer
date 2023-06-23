using UnityEngine;
using UnityEngine.UI;

namespace Gallery {
    /// <summary>
    /// Класс, отвечающий за изменение размера содержимого для подгонки под экран.
    /// </summary>
    public class ResizeContentToFitScreen : MonoBehaviour {
        [SerializeField] private Transform canvas;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;
        [SerializeField] private int spacing = 25;

        /// <summary>
        /// Изменяет размер содержимого, чтобы подогнать его под размер экрана.
        /// </summary>
        public void ResizeContent() {
            Rect canvasRect = canvas.GetComponent<RectTransform>().rect;
            float width = canvasRect.width;
            float cellSize = (width - gridLayoutGroup.padding.left - gridLayoutGroup.padding.right - spacing) / 2;
            gridLayoutGroup.cellSize = new Vector2(cellSize, cellSize);
            gridLayoutGroup.spacing = new Vector2(spacing, spacing);
        }
    }
}