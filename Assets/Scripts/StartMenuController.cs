using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{
    [SerializeField] private GameObject startPanel; // 绑定StartPanel
    [SerializeField] private Button startButton;    // 绑定StartButton
    [SerializeField] private EnemySpawner enemySpawner; // 绑定敌人生成器

    void Start()
    {
        // 初始化时暂停游戏
        Time.timeScale = 0;
        startButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        // 隐藏开始面板，恢复游戏
        startPanel.SetActive(false);
        Time.timeScale = 1;
        enemySpawner.StartSpawning(); // 手动触发敌人生成
    }
}