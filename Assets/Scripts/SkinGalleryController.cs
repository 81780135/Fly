using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkinGalleryController : MonoBehaviour
{
    [Header("��������")]
    public Sprite[] skinSprites;
    public CanvasGroup imageCanvasGroup;
    public float fadeDuration = 0.5f;
    public AudioClip switchSound; // ������Ч�ļ�

    [Header("��������")]
    public Image displayImage;
    public Button leftButton;
    public Button rightButton;

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
        AudioSource.PlayClipAtPoint(switchSound, Camera.main.transform.position);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            imageCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        imageCanvasGroup.alpha = endAlpha;
    }

    private void UpdateDisplay()
    {
        if (skinSprites.Length > 0)
        {
            displayImage.sprite = skinSprites[currentIndex];
        }
    }

}