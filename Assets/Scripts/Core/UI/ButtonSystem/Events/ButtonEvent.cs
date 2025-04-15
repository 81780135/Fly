using System;

// 文件路径：Assets/Scripts/Core/UI/ButtonSystem/Events/ButtonEvent.cs
public enum ButtonType
{
    StartGame,
    Settings,
    Exit
}
/// <summary>
/// 按钮事件基类，继承 EventArgs 以兼容 EventBus
/// </summary>
public class ButtonEvent : EventArgs
{
    public ButtonType ButtonType { get; }  // 使用枚举类型
    //public string ButtonID { get; }       // 按钮唯一标识（如"StartGame"）
    public DateTime TriggerTime { get; }  // 事件触发时间（用于日志或调试）

    public ButtonEvent(ButtonType ButtonType)
    {
        this.ButtonType = ButtonType;
        TriggerTime = DateTime.Now;
    }
}