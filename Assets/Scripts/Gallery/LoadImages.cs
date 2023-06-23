using UnityEngine;
using UnityEngine.UI;
namespace Gallery {
    public class LoadImages : MonoBehaviour {
        [SerializeField] private Transform canvas;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;
        [SerializeField] private Transform imageItemPrefab; 
        [SerializeField] private ResizeContentToFitScreen resizeContentToFitScreen;

        private int _currentRow;
        
        private float _nextBorderLoad;
        private float _oneHeightItem;
        private void Start() {
            resizeContentToFitScreen.ResizeContent();

            RenderImages();

        }
        private void Update() {
            if (transform.localPosition.y > _nextBorderLoad && _currentRow < 33)
            {
                for (int j = 0; j < 2; j++)
                {
                    Transform newImageItemTransform = Instantiate(imageItemPrefab, GetComponent<Transform>());
                    if (newImageItemTransform.TryGetComponent(out ImageItem newImageItem))
                    {
                        newImageItem.SetImageItemParameters(_currentRow * 2 + j + 1);
                    }
                }
                _currentRow++;
                _nextBorderLoad += _oneHeightItem;
            }
        }
        private void RenderImages() {

            Rect canvasRect = canvas.GetComponent<RectTransform>().rect;
       
            float height = canvasRect.height;

            _currentRow = 0;
       

            _oneHeightItem = gridLayoutGroup.cellSize.y + gridLayoutGroup.spacing.y;
            _nextBorderLoad = 0;

            if (StateGalleryToViewer.CurrentPosition != 0)
            {
                height += StateGalleryToViewer.CurrentPosition;
            }
            for (int i = 0; i < height / _oneHeightItem; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Transform newImageItemTransform = Instantiate(imageItemPrefab, GetComponent<Transform>());
                    if (newImageItemTransform.TryGetComponent(out ImageItem newImageItem))
                    {
                        newImageItem.SetImageItemParameters(i * 2 + j + 1);
                    }
                }
                _currentRow++;
            }
            if (StateGalleryToViewer.CurrentPosition != 0)
            {
                var position = transform.position;
                transform.localPosition = new Vector3(position.x, StateGalleryToViewer.CurrentPosition, position.z);
            }
        }
    }
}