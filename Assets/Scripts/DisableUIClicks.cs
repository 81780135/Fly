using UnityEngine;
using UnityEngine.EventSystems;

public class DisableUIClicks : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // 阻止事件穿透到下层
        eventData.Use();
    }
}