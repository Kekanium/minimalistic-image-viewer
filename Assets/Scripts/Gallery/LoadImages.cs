using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LoadImages : MonoBehaviour {
    [SerializeField] private Transform canvas;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private Transform imageItemPrefab;

    private List<Transform> imagesTransform;

    private int currentRow;
    private float nextBorderLoad;
    private float oneHeightItem;
    private void Start() {
        Rect canvasRect = canvas.GetComponent<RectTransform>().rect;
        float width = canvasRect.width;
        float height = canvasRect.height;

        currentRow = 0;
        imagesTransform = new List<Transform>();

        oneHeightItem = gridLayoutGroup.cellSize.y + gridLayoutGroup.spacing.y;
        nextBorderLoad = 0;

        if (StateGalleryToViewer.CurrentPosition != 0)
        {
            print(StateGalleryToViewer.CurrentPosition.ToString());
            height += StateGalleryToViewer.CurrentPosition;
        }
        for (int i = 0; i < height / oneHeightItem; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Transform newImageItemTransform = Instantiate(imageItemPrefab, this.GetComponent<Transform>());
                if (newImageItemTransform.TryGetComponent<ImageItem>(out ImageItem newImageItem))
                {
                    newImageItem.SetImageItemParameters(i * 2 + j + 1);
                }
            }
            currentRow++;
        }
        if (StateGalleryToViewer.CurrentPosition != 0)
        {
            transform.localPosition = new Vector3(transform.position.x, StateGalleryToViewer.CurrentPosition, transform.position.z);
        }

    }
    private void Update() {
        if (this.transform.localPosition.y > nextBorderLoad && currentRow<33)
        {
            for (int j = 0; j < 2; j++)
            {
                Transform newImageItemTransform = Instantiate(imageItemPrefab, this.GetComponent<Transform>());
                if (newImageItemTransform.TryGetComponent<ImageItem>(out ImageItem newImageItem))
                {
                    newImageItem.SetImageItemParameters(currentRow * 2 + j + 1);
                }
            }
            currentRow++;
            nextBorderLoad += oneHeightItem;
        }
    }

}