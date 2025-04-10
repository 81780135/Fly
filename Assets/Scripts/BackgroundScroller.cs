using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.5f; // 滚动速度
    private Renderer backgroundRenderer;
    private Vector2 savedOffset;

    void Start()
    {
        backgroundRenderer = GetComponent<Renderer>();
        savedOffset = backgroundRenderer.material.mainTextureOffset;
    }

    void Update()
    {
        // 计算新的UV偏移
        float yOffset = Time.time * scrollSpeed;
        Vector2 offset = new Vector2(0, yOffset);
        backgroundRenderer.material.mainTextureOffset = offset;
    }

    void OnDisable()
    {
        // 重置偏移（可选）
        backgroundRenderer.material.mainTextureOffset = savedOffset;
    }
}