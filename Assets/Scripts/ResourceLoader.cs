using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ResourceLoader
{
    private static Dictionary<string, Sprite> _planeImageCache = new();
    private static Dictionary<int, Sprite> _qualityIconCache = new();

    public static void LoadPlaneImage(string imageID)
    {
        // 原有飞机图片加载逻辑...
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