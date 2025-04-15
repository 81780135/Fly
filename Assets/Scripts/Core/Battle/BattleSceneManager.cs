using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSceneManager : MonoBehaviour
{
    // 配置字段（需在Unity Inspector中绑定）
    [Header("玩家设置")]
    [SerializeField] private GameObject playerPrefab;    // 玩家预制体
    [SerializeField] private Transform playerSpawnPoint; // 玩家生成位置

    [Header("敌人生成器")]
    [SerializeField] private EnemySpawner enemySpawner;  // 敌人生成器组件

    [Header("背景滚动")]
    [SerializeField] private BackgroundScroller scrollingBackground; // 背景滚动脚本

    [Header("UI绑定")]
    [SerializeField] private HealthBar healthBar;        // 血条组件
    private void Awake()
    {
        // 确保单例模式（如果跨场景保留）
        if (FindObjectsOfType<BattleSceneManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        InitializePlayer();
        InitializeEnemySpawner();
        InitializeUI();
        // StartBackgroundScrolling();
    }

    // 初始化玩家
    private void InitializePlayer()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("玩家预制体未配置！");
            return;
        }

        // 实例化玩家并设置位置
        GameObject player = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        PlayerController playerController = player.GetComponent<PlayerController>();

        // 绑定血条
        if (healthBar != null && playerController != null)
        {
            healthBar.SetTarget(playerController);
        }
        else
        {
            Debug.LogError("血条或玩家控制器未绑定！");
        }
    }

    // 初始化敌人生成器
    private void InitializeEnemySpawner()
    {
        if (enemySpawner == null)
        {
            Debug.LogError("敌人生成器未绑定！");
            return;
        }
        enemySpawner.StartSpawning(); // 调用生成器的启动方法
    }

    // 初始化UI
    private void InitializeUI()
    {
        // 更新钻石显示（示例）
        //UIManager.Instance.UpdateDiamond(DataManager.Instance.Diamond);
    }
    // private void InitializeBackground()
    // {
    //     // 从Excel配置中获取当前关卡背景路径
    //     string bgPath = ConfigLoader.GetCurrentLevelBackgroundPath();
    //     Material bgMat = Resources.Load<Material>(bgPath);

    //     if (scrollingBackground != null && bgMat != null)
    //     {
    //         scrollingBackground.SetBackgroundMaterial(bgMat);
    //     }
    // }

    // 返回主菜单（示例）
    public void ReturnToMainMenu()
    {
        Destroy((gameObject));
        SceneManager.LoadScene("MainMenu");
    }
}