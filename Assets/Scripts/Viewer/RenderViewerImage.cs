using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderViewerImage : MonoBehaviour {
    [SerializeField] private Image image;


    private Vector2 _savedSizeDelta;
    private Vector3 _savedPosition;
    private Vector3 _savedScale;
    private Quaternion _savedRotation;



    private int numberImage;
    void Start() {
        RectTransform rectTransform = GetComponent<RectTransform>();

        // Сохраняем параметры компонента RectTransform


        _savedPosition = rectTransform.position;
        _savedSizeDelta = rectTransform.sizeDelta;
        _savedScale = rectTransform.localScale;
        _savedRotation = rectTransform.localRotation;

        

        Texture2D _texture = StateGalleryToViewer.PickedTexture2D;

        Sprite sprite = Sprite.Create(_texture, new Rect(0, 0, _texture.width, _texture.height), Vector2.one * 0.5f);

        // Присваиваем спрайт компоненту Image
        //Image image = GetComponent<Image>();
        image.sprite = sprite;
        image.color = Color.white;

        image.preserveAspect = true;


    }
    public void ResetImage() {
        RectTransform rectTransform = GetComponent<RectTransform>();

        // Восстанавливаем сохраненные параметры компонента RectTransform

        rectTransform.position = _savedPosition;
        rectTransform.sizeDelta = _savedSizeDelta;
        rectTransform.localScale = _savedScale;
        rectTransform.localRotation = _savedRotation;


    }




}