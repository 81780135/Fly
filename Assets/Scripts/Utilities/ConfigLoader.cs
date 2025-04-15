using UnityEngine;
using System.Collections.Generic;
using System.IO;

public static class ConfigLoader
{
    // 示例：从CSV加载敌人配置
    public static List<EnemyConfig> LoadEnemyConfig()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("Configs/EnemyConfig");
        string[] lines = csvFile.text.Split('\n');
        List<EnemyConfig> configs = new List<EnemyConfig>();

        for (int i = 1; i < lines.Length; i++) // 跳过表头
        {
            string[] fields = lines[i].Split(',');
            if (fields.Length < 3) continue;

            EnemyConfig config = new EnemyConfig
            {
                //EnemyID = fields[0],
                //Health = int.Parse(fields[1]),
                //Speed = float.Parse(fields[2])
            };
            configs.Add(config);
        }
        return configs;
    }

    // 示例：从JSON加载子弹配置
    // public static BulletConfig LoadBulletConfig()
    // {
    //     TextAsset jsonFile = Resources.Load<TextAsset>("Configs/BulletTypes");
    //     return JsonUtility.FromJson<BulletConfig>(jsonFile.text);
    // }

    // // 从StreamingAssets加载（需协程）
    // public static IEnumerator LoadLevelConfigAsync(string fileName)
    // {
    //     string path = Path.Combine(Application.streamingAssetsPath, fileName);
    //     using (UnityWebRequest request = UnityWebRequest.Get(path))
    //     {
    //         yield return request.SendWebRequest();
    //         if (request.result == UnityWebRequest.Result.Success)
    //         {
    //             LevelConfig config = JsonUtility.FromJson<LevelConfig>(request.downloadHandler.text);
    //             // 使用配置...
    //         }
    //     }
    // }
}

// 数据类定义
[System.Serializable]
public class EnemyConfig
{
    public string EnemyID;
    public int Health;
    public float Speed;
}

// [System.Serializable]
// public class BulletConfig
// {
//     public List<BulletType> Types;
// }

[System.Serializable]
public class BulletType
{
    public string Name;
    public float Damage;
    public float Speed;
}