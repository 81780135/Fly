using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ResourceLoader
{
    private static Dictionary<int, Sprite> _planeImageCache = new();
    private static Dictionary<int, Sprite> _qualityIconCache = new();

    public static Sprite LoadPlaneImage(int resource)
    {
        if(_planeImageCache.TryGetValue(resource, out var cachedIcon))
            return cachedIcon;

        string path = $"Plane/show/{resource}";
        Sprite sprite = Resources.Load<Sprite>(path);

        if(sprite == null)
        {
            sprite = Resources.Load<Sprite>("Plane/show/default");
        }

        _planeImageCache.Add(resource, sprite);
        return sprite; 
        
    }

    public static Sprite LoadQualityIcon(int quality)
    {
        if(_qualityIconCache.TryGetValue(quality, out var cachedIcon))
            return cachedIcon;

        string path = $"Plane/quality/{quality}";
        Sprite sprite = Resources.Load<Sprite>(path);

        if(sprite == null)
        {
            Debug.LogError($"找不到品质图标：{path}");
            sprite = Resources.Load<Sprite>("Plane/quality/default");
        }

        _qualityIconCache.Add(quality, sprite);
        return sprite;
    }
}