using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI diamondText;
    
    // 主菜单UI组件
    public GameObject mainMenuPanel;
    // 战斗场景UI组件
    public GameObject battleHUD;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // 监听场景加载事件
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateHealth(int health)
    {
        healthText.text = $"体力: {health}/20";
    }

    public void UpdateDiamond(int diamond)
    {
        diamondText.text = $"钻石: {diamond}";
    }
    
    public GameObject pausePanel;

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    
    // 场景加载完成后触发
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 根据场景名切换UI
        switch (scene.name)
        {
            case "MainMenu":
                mainMenuPanel = GameObject.Find("MainMenuPanelCanvas");
                battleHUD = null; // 清除非当前场景的UI引用
                break;
            case "BattleScene":
                battleHUD = GameObject.Find("BattleHUDCanvas");
                mainMenuPanel = null;
                break;
        }
        // 根据场景切换UI显示状态
        UpdateUIVisibility(scene.name);
    }
    
    // 示例方法：更新血条
    public void UpdateHealthBar(int health)
    {
        // 绑定战斗场景的血条组件
        if (battleHUD != null)
            battleHUD.GetComponent<HealthBar>().SetValue(health);
    }
    
    // 更新UI显示状态
    private void UpdateUIVisibility(string sceneName)
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(sceneName == "MainMenu");
        if (battleHUD != null) battleHUD.SetActive(sceneName == "BattleScene");
    }
    // 清理事件订阅
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    private void OnEnable()
    {
        EventBus.Subscribe<PlayerDeathEvent>(OnPlayerDeath);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<PlayerDeathEvent>(OnPlayerDeath);
    }
    
    private void OnPlayerDeath(PlayerDeathEvent e)
    {
        ConfirmationPanel.Show("你死亡了，是否返回主界面？",() => SceneManager.LoadScene("MainMenu"),() => SceneManager.LoadScene("MainMenu"));
    }
}