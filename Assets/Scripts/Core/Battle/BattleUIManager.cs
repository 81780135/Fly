using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    // 战斗场景UI组件
    public GameObject battleHUD;
    
        
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
    
        
    // 示例方法：更新血条
    public void UpdateHealthBar(int health)
    {
        // 绑定战斗场景的血条组件
        if (battleHUD != null)
            battleHUD.GetComponent<HealthBar>().SetValue(health);
    }
    
        
    private void OnPlayerDeath(PlayerDeathEvent e)
    {
        ConfirmationPanel.Show("你死亡了，是否返回主界面？",() => SceneManager.LoadScene("MainMenu"),() => SceneManager.LoadScene("MainMenu"));
    }
    // 清理事件订阅
    private void OnDestroy()
    {
        EventBus.Unsubscribe<PlayerDeathEvent>(OnPlayerDeath);
    }
    
    private void OnEnable()
    {
        EventBus.Subscribe<PlayerDeathEvent>(OnPlayerDeath);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<PlayerDeathEvent>(OnPlayerDeath);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
