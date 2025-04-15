using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("绑定组件")]
    [SerializeField] private Slider slider;          // Slider组件
    [SerializeField] private PlayerController player; // 玩家控制器

    [Header("平滑过渡")]
    [SerializeField] private float smoothSpeed = 5f; // 血量变化平滑速度
    private float targetHealth;                      // 目标血量值

    void Start()
    {
        if (slider == null)
        {
            slider = GetComponent<Slider>();
            if (slider == null)
            {
                Debug.LogError("未找到Slider组件！");
                return;
            }
        }

        if (player == null)
        {
            Debug.LogError("玩家控制器未绑定！");
            return;
        }

        // 初始化血条最大值
        slider.maxValue = player.MaxHealth;
        slider.value = player.CurrentHealth;
        targetHealth = player.CurrentHealth;
    }

    void Update()
    {
        // 平滑过渡到目标血量
        if (slider.value != targetHealth)
        {
            slider.value = Mathf.Lerp(slider.value, targetHealth, smoothSpeed * Time.deltaTime);
        }
 
        // 实时更新目标血量（防止玩家血量外部修改）
        targetHealth = player.CurrentHealth;
    }

    // 外部调用：直接绑定玩家（如场景初始化时调用）
    public void SetTarget(PlayerController targetPlayer)
    {
        player = targetPlayer;
        slider.maxValue = player.MaxHealth;
        slider.value = player.CurrentHealth;
        targetHealth = player.CurrentHealth;
    }

    void OnEnable()
    {
        EventBus.Subscribe<PlayerHealthChangedEvent>(OnHealthChanged);
    }

    void OnDisable()
    {
        EventBus.Unsubscribe<PlayerHealthChangedEvent>(OnHealthChanged);
    }

    private void OnHealthChanged(PlayerHealthChangedEvent e)
    {
        targetHealth = e.NewHealth;
    }

    public void SetValue(float value)
    {
        targetHealth = value;
    }
    
    
}