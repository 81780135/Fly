public class StartGameButtonController : ButtonController
{
    protected override void OnButtonClicked()
    {
        // 发布基础按钮事件
        EventBus.Publish(new ButtonEvent(ButtonType.StartGame));

        // 如果需要传递额外数据
        // EventBus.Publish(new ButtonWithDataEvent("StartGame", new { Level = 1 }));

        // 执行场景切换逻辑
        SceneLoader.LoadScene("BattleScene");
    }
}