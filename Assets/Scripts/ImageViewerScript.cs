using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageViewerScript : MonoBehaviour
{
    public RawImage imageDisplay;

    public void SetImage(Texture2D texture)
    {
        imageDisplay.texture = texture;
    }
}
