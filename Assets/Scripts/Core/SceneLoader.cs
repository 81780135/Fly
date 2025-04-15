// 文件路径：Assets/Scripts/Core/SceneManagement/SceneLoader.cs
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    // 静态方法：直接调用无需实例化
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // 可选：异步加载场景（带加载界面）
    public static void LoadSceneAsync(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}