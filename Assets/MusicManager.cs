using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �л�����ʱ������
            GetComponent<AudioSource>().Play(); // �Զ���������
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ֹͣ���ֵķ���
    public void StopMusic()
    {
        GetComponent<AudioSource>().Stop();
    }
}