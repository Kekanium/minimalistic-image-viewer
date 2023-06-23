using System.IO;
using UnityEngine;

namespace Gallery {
    /// <summary>
    /// Класс, отвечающий за сохранение и получение изображений.
    /// </summary>
    public class ImageSaver : MonoBehaviour {
        private const string DirectoryFolder = "./Cache";

        /// <summary>
        /// Сохраняет изображение в файл.
        /// </summary>
        /// <param name="texture">Текстура изображения.</param>
        /// <param name="imageNumber">Номер изображения.</param>
        public static void SaveImage(Texture2D texture, int imageNumber) {
            string fullPathFile = $"{DirectoryFolder}/Image_{imageNumber}.png";
            if (!Directory.Exists(DirectoryFolder))
                Directory.CreateDirectory(DirectoryFolder);

            byte[] textureData = texture.EncodeToPNG();
            File.WriteAllBytes(fullPathFile, textureData);
        }

        /// <summary>
        /// Получает изображение по его номеру.
        /// </summary>
        /// <param name="imageNumber">Номер изображения.</param>
        /// <returns>Текстура изображения или null, если изображение не найдено.</returns>
        public static Texture2D GetImage(int imageNumber) {
            string fullPathFile = $"{DirectoryFolder}/Image_{imageNumber}.png";
            if (!File.Exists(fullPathFile))
                return null;

            byte[] fileData = File.ReadAllBytes(fullPathFile);

            // Создание новой текстуры
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);

            return texture;
        }
    }
}