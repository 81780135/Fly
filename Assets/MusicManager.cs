using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 切换场景时不销毁
            GetComponent<AudioSource>().Play(); // 自动播放音乐
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 停止音乐的方法
    public void StopMusic()
    {
        GetComponent<AudioSource>().Stop();
    }
}