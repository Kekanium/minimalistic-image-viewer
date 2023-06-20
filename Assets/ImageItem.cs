using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageItem : MonoBehaviour, IPointerClickHandler {
    private string imagesURL = "http://data.ikppbb.com/test-task-unity-data/pics/";
    public void SetImageItemParameters(int numberImage) {
        this.name = "Image_" + numberImage.ToString();
        
        print(imagesURL+numberImage.ToString()+".jpg");
        StartCoroutine(GetImageFromServer(numberImage));


    }
    IEnumerator GetImageFromServer(int numberImage)
    {
        Image image = GetComponent<Image>();
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(imagesURL+numberImage.ToString()+".jpg"))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Получаем текстуру из загруженных данных
                Texture2D texture = DownloadHandlerTexture.GetContent(www);

                // Создаем спрайт из текстуры
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

                // Присваиваем спрайт компоненту Image
                image.sprite = sprite;
            }
            else
            {
                Debug.Log("Ошибка загрузки изображения: " + www.error);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        print(name);
    }
}