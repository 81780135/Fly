using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonView : MonoBehaviour
{
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private Button button;

    // 初始化按钮显示
    public void Initialize(ButtonModel model)
    {
        buttonText.text = model.displayText;
        button.interactable = model.isInteractable;
    }

    // 绑定点击事件
    public void BindClick(Action onClick)
    {
        button.onClick.AddListener(() => onClick?.Invoke());
    }
}