using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject mainMenu;   // 主界面
    public GameObject planeMenu; // 战机界面
    public GameObject pveMenu; // 探索关卡界面

    // 当点击"飞机养成"按钮时调用
    public void OpenPlaneMenu()
    {
        Debug.Log("按钮被点击了！"); // 添加这行
        mainMenu.SetActive(false);  // 关闭主界面
        planeMenu.SetActive(true);  // 打开战机界面
    }

    // 当点击"探索关卡"按钮时调用
    public void OpenPveMenu()
    {
        Debug.Log("按钮被点击了！"); // 添加这行
        mainMenu.SetActive(false);  // 关闭主界面
        planeMenu.SetActive(false);  // 关闭战机界面
        pveMenu.SetActive(true);  // 打开探索关卡界面
    }
    
    public void ReturnToMainMenu()
    {
        pveMenu.SetActive(false);   
        planeMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public static UIManager Instance;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI diamondText;
    
    // 主菜单UI组件
    public GameObject mainMenuPanel;

    public void UpdateHealth(int health)
    {
        healthText.text = $"体力: {health}/20";
    }

    public void UpdateDiamond(int diamond)
    {
        diamondText.text = $"钻石: {diamond}";
    }


}