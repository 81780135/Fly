using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ConfirmationPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;
    public static void Show(string message, Action confirmAction, System.Action cancelAction = null)
    {
        GameObject prefab = Resources.Load<GameObject>("UI/ConfirmationPanel");
        GameObject instance = Instantiate(prefab, GameObject.Find("BattleHUDCanvas").transform);
        ConfirmationPanel panel = instance.GetComponent<ConfirmationPanel>();
        panel.Initialize(message, confirmAction, cancelAction);
        instance.SetActive(true);
        Time.timeScale = 0; // 暂停游戏
    }

    private void Initialize(string message, Action confirmAction, Action cancelAction)
    {
        messageText.text = message;
        confirmButton.onClick.AddListener(() => {
            confirmAction?.Invoke();
            Destroy(gameObject);
            Time.timeScale = 1;
        });
        cancelButton.onClick.AddListener(() =>
        {
            Destroy(gameObject);
            Time.timeScale = 1;
        });
    }
}