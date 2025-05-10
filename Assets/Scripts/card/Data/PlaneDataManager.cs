using System.Collections.Generic;
using UnityEngine;

public class PlaneDataManager : MonoBehaviour
{
    public static PlaneDataManager Instance;
    // public TextAsset configFile;
    private Dictionary<int, List<PlaneData>> planeDataDict = new Dictionary<int, List<PlaneData>>();
    public List<PlaneData> allCharacters = new List<PlaneData>();

    private void Awake()
    {
        Instance = this;
        LoadCSVData();
    }

    void Start()
    {
        
    }

    void LoadCSVData()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("Configs/飞机表");
        string[] lines = csvFile.text.Split('\n');
        
        for (int i = 1; i <lines.Length; i++) // 从索引1开始跳过表头
        {
            string[] values = lines[i].Split(',');
            PlaneData planeData = new PlaneData();
            planeData.PlaneID = int.Parse(values[0]);
            planeData.Name = values[1].Trim();
            planeData.Quality = int.Parse(values[2]);
            planeData.Level = int.Parse(values[3]);
            planeData.Attack = int.Parse(values[4]);
            planeData.Defense = int.Parse(values[5]);
            planeData.HP = int.Parse(values[6]);
            planeData.resource = int.Parse(values[7]);
            planeData.combat = int.Parse(values[8]);
            allCharacters.Add(planeData);
        }
    }

    void LoadConfig()
    {
        /*PlaneData[] allData = JsonHelper.FromJson<PlaneData>(configFile.text);
        
        foreach (PlaneData data in allData)
        {
            if (!planeDataDict.ContainsKey(data.PlaneID))
            {
                planeDataDict.Add(data.PlaneID, new List<PlaneData>());
            }
            planeDataDict[data.PlaneID].Add(data);
        }*/
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

    public List<PlaneData> GetAllPlaneData()
    {
        return allCharacters;
    }
}