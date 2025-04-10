using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("游戏状态")]
    [SerializeField] GameObject gameOverUI;
    
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // 订阅场景加载事件
        }
        else
        {
            Destroy(gameObject);
        }
    }

        // 场景加载完成后调用此方法
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 重新查找并绑定新场景的gameOverUI
        gameOverUI = GameObject.Find("StartMenuCanvas"); // 确保UI对象名为"GameOverUI"
        // if (gameOverUI != null)
        //     gameOverUI.SetActive(false); // 默认隐藏
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        if (gameOverUI != null)
            gameOverUI.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // if(gameOverUI == null)
        //  gameOverUI =
        gameOverUI.SetActive(false);
    }


    void OnDestroy()
    {
        // 取消订阅事件，防止内存泄漏
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}