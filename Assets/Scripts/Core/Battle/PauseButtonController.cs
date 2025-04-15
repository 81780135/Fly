using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseButtonController : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnPauseClicked);
    }

    private void OnPauseClicked()
    {
        // 静态方法创建面板，无需手动管理实例
        ConfirmationPanel.Show("返回主菜单？", () => SceneManager.LoadScene("MainMenu"));
    }
}