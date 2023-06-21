using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;
public class ImageItem : MonoBehaviour, IPointerClickHandler {
    private const string ImagesURL = "http://data.ikppbb.com/test-task-unity-data/pics/";
    private int numberImage;
    private Texture2D _texture;


    public void OnPointerClick(PointerEventData eventData) {
        Transform contentTransform = transform.parent.transform;
        StateGalleryToViewer.CurrentPosition = contentTransform.position.y;

        StateGalleryToViewer.PickedImage = numberImage;
        StateGalleryToViewer.PickedTexture2D = _texture;
    }
    public void SetImageItemParameters(int number) {
        numberImage = number;
        name = "Image_" + number;

        if ((_texture = ImageSaver.GetImage(number)) == null)
            StartCoroutine(SetImageFromServer(number));
        else
        {
            Sprite sprite = Sprite.Create(_texture, new Rect(0, 0, _texture.width, _texture.height), Vector2.one * 0.5f);

            // Присваиваем спрайт компоненту Image
            Image image = GetComponent<Image>();
            image.sprite = sprite;
            image.color = Color.white;
        }


    }
    private IEnumerator SetImageFromServer(int numberImage) {

        using UnityWebRequest www = UnityWebRequestTexture.GetTexture(ImagesURL + numberImage + ".jpg");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            // Получаем текстуру из загруженных данных
            _texture = DownloadHandlerTexture.GetContent(www);
            // Создаем спрайт из текстуры
            Sprite sprite = Sprite.Create(_texture, new Rect(0, 0, _texture.width, _texture.height), Vector2.one * 0.5f);

            // Присваиваем спрайт компоненту Image
            Image image = GetComponent<Image>();
            image.sprite = sprite;
            image.color = Color.white;

            ImageSaver.SaveImage(_texture, numberImage);

        }
        else
        {
            Debug.Log("Ошибка загрузки изображения: " + www.error);
        }
    }
}