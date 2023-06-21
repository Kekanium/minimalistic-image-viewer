using System.IO;
using UnityEngine;

public class ImageSaver : MonoBehaviour {
    private const string DirectoryFolder = "./Cache";
  
    public static void SaveImage(Texture2D texture, int imageNumber) {
        string FullPathFile = $"{DirectoryFolder}/Image_{imageNumber}.png";
        if (!Directory.Exists(DirectoryFolder))

            Directory.CreateDirectory(DirectoryFolder);

        // Сохраняем текстуру в файл
        byte[] textureData = texture.EncodeToPNG();

        File.WriteAllBytes(FullPathFile, textureData);

        Debug.Log($"Изображение сохранено по пути: {FullPathFile}");
    }
    public static Texture2D GetImage(int imageNumber) {
        string FullPathFile = $"{DirectoryFolder}/Image_{imageNumber}.png";
        if (!File.Exists(FullPathFile))
            return null;
        // Загрузка данных из файла
        byte[] fileData = File.ReadAllBytes(FullPathFile);

        // Создание новой текстуры
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData); // Загрузка данных из файла в текстуру

        return texture;
    }
}