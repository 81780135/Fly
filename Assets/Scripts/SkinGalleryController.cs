using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class SkinGalleryController : MonoBehaviour
{
    [Header("��������")]
    public Sprite[] skinSprites;
    //public CanvasGroup imageCanvasGroup;
    //public float fadeDuration = 0.5f;

    [Header("��Ч")]
    public AudioClip switchSound; // ������Ч�ļ�

    [Header("��������")]
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
        // ����ѭ��ģʽ����Ҫ���ð�ť
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

        // ��Э�̿�ʼ�����
        AudioSource.PlayClipAtPoint(switchSound, Camera.main.transform.position);

        isTransitioning = true;

        // ������ǰͼƬ
        yield return StartCoroutine(FadeEffect(1, 0));

        // ������������ѭ������
        currentIndex = GetLoopIndex(currentIndex + direction);
        UpdateDisplay();

        // ������ͼƬ
        yield return StartCoroutine(FadeEffect(0, 1));

        isTransitioning = false;
    }

    // ѭ���������㷽��
    private int GetLoopIndex(int newIndex)
    {
        if (skinSprites.Length == 0) return 0;

        // ���������
        while (newIndex < 0)
        {
            newIndex += skinSprites.Length;
        }

        // ������������
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