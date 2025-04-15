using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    // 玩家数据
    public int Diamond { get; private set; }
    public int Fuel { get; private set; }

    protected override void Awake()
    {
        base.Awake(); // 调用基类Awake方法
        Initialize();
    }

    // 初始化数据（示例）
    public void Initialize()
    {
        Diamond = 50;  // 初始赠送50钻石
        Fuel = 100;
    }

    // 更新钻石
    public void AddDiamond(int amount)
    {
        Diamond += amount;
        PlayerPrefs.SetInt("Diamond", Diamond);
    }
}