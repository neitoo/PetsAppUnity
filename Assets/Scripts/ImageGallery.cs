using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ImageGallery", menuName = "Data/ImageGallery")]
public class ImageGallery : ScriptableObject
{
    public List<Texture2D> images = new List<Texture2D>();
    public HashSet<int> loadedIndices = new HashSet<int>();
}

