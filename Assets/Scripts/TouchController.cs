using UnityEngine;

public class TouchController : MonoBehaviour
{
    private Vector2 touchStartPos;    // 初始触摸位置
    private Vector2 touchCurrentPos;  // 当前触摸位置
    private bool isTouching = false;   // 是否正在触摸
    // 新增标志位
    public bool IsInputActive => isTouching;

    void Update()
    {

        // 移动端触控检测
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            HandleInput(touch.position,touch.phase);
        }
        // PC端鼠标检测
        else if (Application.isEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleInput(Input.mousePosition, TouchPhase.Began);
            }
            else if (Input.GetMouseButton(0))
            {
                HandleInput(Input.mousePosition, TouchPhase.Moved);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                HandleInput(Input.mousePosition, TouchPhase.Ended);
            }
        }

    }

    // 统一处理输入事件
    private void HandleInput(Vector2 inputPos, TouchPhase phase)
    {
        switch (phase)
        {
            case TouchPhase.Began:
                isTouching = true;
                touchStartPos = inputPos;
                touchCurrentPos = inputPos;
                break;

            case TouchPhase.Moved:
            case TouchPhase.Stationary:
                touchCurrentPos = inputPos;
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                isTouching = false;
                break;
        }
    }

    // 获取触控偏移量（用于控制移动方向）
    public Vector2 GetTouchDelta()
    {
        if (!IsInputActive) return Vector2.zero;
        return (touchCurrentPos - touchStartPos).normalized;
    }

    // 获取触控目标位置（用于直接移动）
    public Vector2 GetTouchWorldPosition()
    {
        if (!IsInputActive) return Vector2.negativeInfinity;
        return Camera.main.ScreenToWorldPoint(touchCurrentPos);
    }
}