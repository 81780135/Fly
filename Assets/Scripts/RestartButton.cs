// 将以下脚本挂载到Restart按钮
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => 
        // Debug.Log("点了")
            GameManager.Instance.RestartGame()
        );
    }
}