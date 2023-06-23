using System.IO;
using UnityEngine;

namespace Gallery {
    public class ImageSaver : MonoBehaviour {
        private const string DirectoryFolder = "./Cache";

        public static void SaveImage(Texture2D texture, int imageNumber) {
            string fullPathFile = $"{DirectoryFolder}/Image_{imageNumber}.png";
            if (!Directory.Exists(DirectoryFolder))
                Directory.CreateDirectory(DirectoryFolder);


            byte[] textureData = texture.EncodeToPNG();

            File.WriteAllBytes(fullPathFile, textureData);
        }
        public static Texture2D GetImage(int imageNumber) {
            string fullPathFile = $"{DirectoryFolder}/Image_{imageNumber}.png";
            if (!File.Exists(fullPathFile))
                return null;
       
            byte[] fileData = File.ReadAllBytes(fullPathFile);

            // Создание новой текстуры
            Texture2D texture = new(2, 2);
            texture.LoadImage(fileData);

            return texture;
        }
    }
}