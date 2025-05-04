using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class SkinGalleryController : MonoBehaviour
{
    [Header("基础设置")]
    public Sprite[] skinSprites;
    //public CanvasGroup imageCanvasGroup;
    //public float fadeDuration = 0.5f;

    [Header("音效")]
    public AudioClip switchSound; // 拖入音效文件

    [Header("界面引用")]
    public Image displayImage;
    public Button leftButton;
    public Button rightButton;
    // public int PlaneID;
    // public int Level;
    // public int Attack;
    // public int Defense;
    // public int HP;
    public TextMeshProUGUI m_textLevel;
    public TextMeshProUGUI m_textAttack;
    public TextMeshProUGUI m_textDefense;
    public TextMeshProUGUI m_textHP;
    

    private int currentIndex = 0;
    private bool isTransitioning = false;

    void Start()
    {
        UpdateDisplay();
        // 无限循环模式不需要禁用按钮
        leftButton.interactable = true;
        rightButton.interactable = true;
    }

    public void ShowNext()
    {
        if (!isTransitioning)
            StartCoroutine(SwitchAnimation(1));
    }

    public void ShowPrevious()
    {
        if (!isTransitioning)
            StartCoroutine(SwitchAnimation(-1));
    }

    private IEnumerator SwitchAnimation(int direction)
    {

        // 在协程开始处添加
        AudioSource.PlayClipAtPoint(switchSound, Camera.main.transform.position);

        isTransitioning = true;

        // 淡出当前图片
        yield return StartCoroutine(FadeEffect(1, 0));

        // 计算新索引（循环处理）
        currentIndex = GetLoopIndex(currentIndex + direction);
        UpdateDisplay();

        // 淡入新图片
        yield return StartCoroutine(FadeEffect(0, 1));

        isTransitioning = false;
    }

    // 循环索引计算方法
    private int GetLoopIndex(int newIndex)
    {
        if (skinSprites.Length == 0) return 0;

        // 处理负数情况
        while (newIndex < 0)
        {
            newIndex += skinSprites.Length;
        }

        // 处理正数超限
        return newIndex % skinSprites.Length;
    }

    private IEnumerator FadeEffect(float startAlpha, float endAlpha)
    {
        //AudioSource.PlayClipAtPoint(switchSound, Camera.main.transform.position);

       // float elapsed = 0f;
       // while (elapsed < fadeDuration)
        {
            //imageCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            //elapsed += Time.deltaTime;
            yield return null;
        }
        //imageCanvasGroup.alpha = endAlpha;
    }

    private void UpdateDisplay()
    {
        if (skinSprites.Length > 0)
        {
            displayImage.sprite = skinSprites[currentIndex];
        }

    
        List<PlaneData> allCharacters = PlaneDataManager.Instance.GetAllPlaneData();
        
        m_textLevel.text = allCharacters[currentIndex].Level.ToString();
        m_textHP.text = allCharacters[currentIndex].HP.ToString();
        m_textAttack.text = allCharacters[currentIndex].Attack.ToString();
        m_textDefense.text = allCharacters[currentIndex].Defense.ToString();
    }

}