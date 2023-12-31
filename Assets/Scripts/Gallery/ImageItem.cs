using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;
namespace Gallery {
    /// <summary>
    /// Класс, представляющий элемент изображения в галерее.
    /// </summary>
    public class ImageItem : MonoBehaviour, IPointerClickHandler {
        private const string ImagesURL = "http://data.ikppbb.com/test-task-unity-data/pics/";
        private Texture2D _texture;

        /// <summary>
        /// Обработчик события щелчка указателем мыши на элементе изображения.
        /// </summary>
        /// <param name="eventData">Данные о событии щелчка указателем мыши.</param>
        public void OnPointerClick(PointerEventData eventData) {
            Transform contentTransform = transform.parent.transform;
            StateGalleryToViewer.CurrentPosition = contentTransform.localPosition.y;
            StateGalleryToViewer.PickedTexture2D = _texture;

            SceneTransition.SwitchToScene("Viewer");
        }
        
        /// <summary>
        /// Устанавливает параметры элемента изображения.
        /// </summary>
        /// <param name="number">Номер изображения.</param>
        public void SetImageItemParameters(int number) {
            name = $"Image_{number}";

            if ((_texture = ImageSaver.GetImage(number)) == null)
            {
                StartCoroutine(SetImageFromServer(number));
            }
            else
            {
                Sprite sprite = Sprite.Create(_texture, new Rect(0, 0, _texture.width, _texture.height), Vector2.one * 0.5f);

                // Присваиваем спрайт компоненту Image
                Image image = GetComponent<Image>();
                image.sprite = sprite;
                image.color = Color.white;
            }


        }
        /// <summary>
        /// Получает изображение с сервера.
        /// </summary>
        /// <param name="numberImage">Номер изображения.</param>
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
          
        }
    }
}