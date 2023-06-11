using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GalleryScript : MonoBehaviour
{
    public GameObject imagePrefab;
    public RectTransform content;
    public int imagesPerRow = 2;
    public string imageViewerSceneName = "ViewPhotoScene";

    private string baseUrl = "http://data.ikppbb.com/test-task-unity-data/pics/";
    private int totalImages = 66;
    private int currentRow = 0;
    private int currentColumn = 0;
    private float imageWidth;
    private float imageHeight;
    private float spacing;
    private ImageGallery imageGallery;

    private void Start()
    {
        imageWidth = imagePrefab.GetComponent<RectTransform>().sizeDelta.x;
        imageHeight = imagePrefab.GetComponent<RectTransform>().sizeDelta.y;
        spacing = content.GetComponent<GridLayoutGroup>().spacing.x;

        imageGallery = GameManagerScript.imageGallery; // Получаем ссылку на ImageGallery из GameManager

        if (imageGallery.images.Count == 0)
        {
            StartCoroutine(LoadImages());
        }
        else
        {
            // Используем ранее загруженные изображения из ImageGallery
            foreach (Texture2D texture in imageGallery.images)
            {
                AddImage(texture);
            }

            if (GameManagerScript.isLoadingImages)
            {
                // Продолжаем загрузку картинок, если она была прервана
                StartCoroutine(ContinueLoadingImages());
            }
        }
    }

    private IEnumerator ContinueLoadingImages()
    {
        GameManagerScript.isLoadingImages = true;

        while (GameManagerScript.loadedImages < totalImages)
        {
            if (!imageGallery.loadedIndices.Contains(GameManagerScript.loadedImages))
            {
                string imageUrl = baseUrl + (GameManagerScript.loadedImages + 1) + ".jpg";
                UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                    AddImage(texture);

                    // Добавляем загруженное изображение в ImageGallery
                    imageGallery.images.Add(texture);
                    imageGallery.loadedIndices.Add(GameManagerScript.loadedImages);
                }
            }

            GameManagerScript.loadedImages++;
        }

        GameManagerScript.isLoadingImages = false;
    }
    

    private IEnumerator LoadImages()
    {
        GameManagerScript.isLoadingImages = true;
        while (GameManagerScript.loadedImages < totalImages)
        {
            string imageUrl = baseUrl + (GameManagerScript.loadedImages + 1) + ".jpg";
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                AddImage(texture);

                // Добавляем загруженное изображение в ImageGallery
                imageGallery.images.Add(texture);
            }

            GameManagerScript.loadedImages++;
        }
    }

    private void AddImage(Texture2D texture)
    {
        GameObject imageObject = Instantiate(imagePrefab, content);
        Image image = imageObject.GetComponent<Image>();
        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

        Button button = imageObject.GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            OpenImageViewer(texture);
        });
        
        RectTransform imageRectTransform = imageObject.GetComponent<RectTransform>();
        float xOffset = (imageWidth + spacing) * currentColumn;
        float yOffset = -(imageHeight + spacing) * currentRow;
        imageRectTransform.anchoredPosition = new Vector2(xOffset, yOffset);

        currentColumn++;
        if (currentColumn >= imagesPerRow)
        {
            currentColumn = 0;
            currentRow++;
        }
    }

    private void OpenImageViewer(Texture2D texture)
    {
        // Найди объект с скриптом ImageViewerScript в сцене "Image Viewer"
        SceneManager.LoadScene(imageViewerSceneName);
        SceneManager.sceneLoaded += (scene, loadSceneMode) =>
        {
            ImageViewerScript imageViewer = FindObjectOfType<ImageViewerScript>();
            if (imageViewer != null)
            {
                imageViewer.SetImage(texture);
            }
        };
    }
}
