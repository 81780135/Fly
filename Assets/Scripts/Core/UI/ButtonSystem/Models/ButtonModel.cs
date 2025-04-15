[System.Serializable]
public class ButtonModel
{
    public ButtonType ButtonType;      // 按钮唯一标识（如"StartGame"）
    public string displayText;    // 显示文本
    public bool isInteractable = true;
}