using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static ImageGallery imageGallery; // Глобальная переменная для хранения ImageGallery
    public static bool isLoadingImages; // Флаг, указывающий на то, что загрузка изображений выполняется
    public static int loadedImages = 0;

    private void Awake()
    {
        // Проверяем, существует ли ImageGallery, и если нет, создаем новый экземпляр
        if (imageGallery == null)
        {
            imageGallery = ScriptableObject.CreateInstance<ImageGallery>();
        }
    }
}