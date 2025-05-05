using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceLoader : MonoBehaviour
{
    private static Dictionary<string, Sprite> _imageCache = new();

    public static Sprite LoadPlaneImage(string Quality)
    {
        // 优先从缓存读取
        if(_imageCache.TryGetValue(Quality, out var cachedSprite))
        {
            return cachedSprite;
        }

        // 资源路径格式：Resources/Plane/quality/1
        string path = $"Plane/quality/{Quality}";
        Sprite sprite = Resources.Load<Sprite>(path);

        // 加载失败处理
        if(sprite == null)
        {
            Debug.LogWarning($"找不到图片资源：{Quality}");
            sprite = Resources.Load<Sprite>("Plane/quality");
        }

        _imageCache.Add(Quality, sprite);
        return sprite;
    }

    // 预加载所有图片（可选）
    public static void PreloadImages()
    {
        Sprite[] allSprites = Resources.LoadAll<Sprite>("Plane/quality");
        foreach(var sprite in allSprites)
        {
            _imageCache[sprite.name] = sprite;
        }
    }
}