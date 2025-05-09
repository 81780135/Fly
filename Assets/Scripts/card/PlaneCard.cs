using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneCard : MonoBehaviour
{
    [SerializeField] Text idText;
    [SerializeField] Text levelText;
    [SerializeField] Text statsText;
    [SerializeField] Image QualityIcon;
    
    private int currentLevel = 1;
    private int planeID;

    public void Initialize(int id)
    {
        planeID = id;
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        PlaneData data = PlaneDataManager.Instance.GetPlaneData(planeID, currentLevel);

        idText.text = $"ID: {planeID}";
        levelText.text = $"Lv.{currentLevel}";
        statsText.text = $"ATK: {data.Attack}\nDEF: {data.Defense}\nHP: {data.HP}";
        // 更新品质图标（核心变化）
        QualityIcon.sprite = ResourceLoader.LoadQualityIcon(data.Quality);
    }

    // 升级按钮点击事件
    void OnUpgradeClick()
    {
        // 检查是否可以升级
        if (PlaneDataManager.Instance.GetPlaneData(planeID, currentLevel + 1) != null)
        {
            currentLevel++;
            UpdateDisplay();
        }
    }
}