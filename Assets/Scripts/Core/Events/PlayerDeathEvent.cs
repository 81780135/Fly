using System;

public class PlayerDeathEvent : EventArgs
{
    public string Cause { get; } // 可扩展死亡原因（如碰撞、血量耗尽）
    public PlayerDeathEvent(string cause) => Cause = cause;
}