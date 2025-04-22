using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonScaleAnim : MonoBehaviour
{
    [Header("动画设置")]
    public float scaleSize = 0.9f; // 点击时缩放比例
    public float animDuration = 0.1f; // 动画持续时间
    
    private Button targetButton;
    private Vector3 originalScale; // 原始尺寸

    void Start()
    {
        targetButton = GetComponent<Button>();
        originalScale = transform.localScale;
        
        // 绑定点击事件
        targetButton.onClick.AddListener(PlayScaleAnim);
    }

    void PlayScaleAnim()
    {
        StartCoroutine(ScaleAnimation());
    }

    IEnumerator ScaleAnimation()
    {
        // 缩小阶段
        float timer = 0;
        while (timer < animDuration)
        {
            transform.localScale = Vector3.Lerp(
                originalScale, 
                originalScale * scaleSize, 
                timer / animDuration
            );
            timer += Time.deltaTime;
            yield return null;
        }

        // 恢复阶段
        timer = 0;
        while (timer < animDuration)
        {
            transform.localScale = Vector3.Lerp(
                originalScale * scaleSize, 
                originalScale, 
                timer / animDuration
            );
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale; // 确保最终尺寸准确
    }
}