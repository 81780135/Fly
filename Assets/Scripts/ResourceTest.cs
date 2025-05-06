using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceTest : MonoBehaviour
{
    public Image testImage;

    void Start()
    {
        // 测试加载ID 3的图片
        Sprite sprite = ResourceLoader.LoadPlaneImage("1001");
        testImage.sprite = sprite;
        
        // 输出日志查看结果
        if(sprite == null)
        {
            Debug.LogError("测试加载失败！");
        }
    }
}