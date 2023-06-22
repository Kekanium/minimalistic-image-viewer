using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderViewerImage : MonoBehaviour {
    [SerializeField] private Image image;

    private int numberImage;
    void Start() {
        Texture2D _texture = StateGalleryToViewer.PickedTexture2D; 
        
            Sprite sprite = Sprite.Create(_texture, new Rect(0, 0, _texture.width, _texture.height), Vector2.one * 0.5f);

            // Присваиваем спрайт компоненту Image
            //Image image = GetComponent<Image>();
            image.sprite = sprite;
            image.color = Color.white;
        
        image.preserveAspect = true;
    }


}
