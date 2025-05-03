using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlaneData
{
    public int PlaneID;
    public int Level;
    public int Attack;
    public int Defense;
    public int HP;
}

public class PlaneDataManager : MonoBehaviour
{
    public TextAsset configFile;
    private Dictionary<int, List<PlaneData>> planeDataDict = new Dictionary<int, List<PlaneData>>();

    void Start()
    {
        LoadConfig();
    }

    void LoadConfig()
    {
        PlaneData[] allData = JsonHelper.FromJson<PlaneData>(configFile.text);
        
        foreach (PlaneData data in allData)
        {
            if (!planeDataDict.ContainsKey(data.PlaneID))
            {
                planeDataDict.Add(data.PlaneID, new List<PlaneData>());
            }
            planeDataDict[data.PlaneID].Add(data);
        }
    }

    public List<int> GetAllPlaneIDs()
    {
        return new List<int>(planeDataDict.Keys);
    }

    public PlaneData GetPlaneData(int id, int level)
    {
        if (planeDataDict.ContainsKey(id) 
            && level <= planeDataDict[id].Count)
        {
            return planeDataDict[id][level - 1];
        }
        return null;
    }
}

// JSON数组解析辅助类
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}