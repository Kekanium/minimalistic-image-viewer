using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
//[ExecuteInEditMode]
public class ResizeContentToFitScreen : MonoBehaviour {
    [SerializeField] private Transform canvas;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private int spacing=25;
    
    private void FixedUpdate() {
        Rect canvasRect = canvas.GetComponent<RectTransform>().rect;
        float width = canvasRect.width;
        float height = canvasRect.height;
        var cellSize = (width - gridLayoutGroup.padding.left - gridLayoutGroup.padding.right - spacing)/2;
        gridLayoutGroup.cellSize = new Vector2(cellSize, cellSize);
        gridLayoutGroup.spacing = new Vector2(spacing, spacing);


    }


}