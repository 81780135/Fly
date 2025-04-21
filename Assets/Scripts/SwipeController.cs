using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [Header("Settings")]
    public float swipeSensitivity = 0.5f;
    public float smoothTime = 0.2f;
    public float yMin = -500f;
    public float yMax = 500f;
    public float fixedX = -451f;  // 固定的X轴位置

    private RectTransform rectTransform;
    private Vector2 targetPosition;
    private Vector2 currentVelocity;  // 修正变量名

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        // 初始化位置时固定X轴
        targetPosition = new Vector2(fixedX, rectTransform.anchoredPosition.y);
    }

    void Update()
    {
        // 使用Vector2.SmoothDamp的正确写法
        rectTransform.anchoredPosition = Vector2.SmoothDamp(
            current: rectTransform.anchoredPosition,
            target: targetPosition,
            currentVelocity: ref currentVelocity,  // 传递速度引用
            smoothTime: smoothTime
        );
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 计算Y轴移动量（deltaY）
        float deltaY = eventData.delta.y * swipeSensitivity;
        
        // 更新目标位置时保持X轴固定
        targetPosition = new Vector2(
            fixedX,
            Mathf.Clamp(targetPosition.y + deltaY, yMin, yMax)
        );
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 惯性滑动时也保持X轴固定
        float momentumY = eventData.delta.y * 0.1f;
        targetPosition = new Vector2(
            fixedX,
            Mathf.Clamp(targetPosition.y + momentumY, yMin, yMax)
        );
    }
}