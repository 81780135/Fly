using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlaneData
{
    public int PlaneID;
    public string Name;
    public int Quality;
    public int Level;
    public int Attack;
    public int Defense;
    public int HP;
    public int resource;
    public int combat;
}



// JSON数组解析辅助类
/*public static class JsonHelper
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
}*/