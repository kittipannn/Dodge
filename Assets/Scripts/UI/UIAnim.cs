using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIAnim : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] AdsManager adsManager;

    [Header("Menu Panel")]
    [SerializeField] RectTransform menuPanel;
    [SerializeField] RectTransform mainPanel;
    [SerializeField] Image[] mainImage;
    [SerializeField] Image purchasedImage;

    [Header("Shop Panel")]
    [SerializeField] RectTransform infoPanel;
    [SerializeField] Image[] infoImage;
    [SerializeField] Text[] infoText;

    [Header("Ads Panel")]
    [SerializeField] RectTransform adsPanel;
    [SerializeField] Button noThanks;
    [SerializeField] Text noThxText;


    [Header("Result Panel")]
    [SerializeField] RectTransform resultPanel;
    [SerializeField] Image[] resultImage;
    [SerializeField] Text[] resultText;


    void Start()
    {
        // StartGame
        mainPanel.localScale = new Vector2(0, 0);
        Sequence mainSequence = DOTween.Sequence();
        mainSequence.Append(mainPanel.transform.DOScale(new Vector2(1f, 1f), 1.5f).SetEase(Ease.InOutCubic))
            .Append(menuPanel.gameObject.GetComponent<Image>().DOFade(0, 2))
            .OnComplete(adsManager.showBannerAds);
    }

    public void playGameTween()
    {
        // Click PlayBtn
        UiManager uiManager = FindObjectOfType<UiManager>();
        Sequence inGameSequence = DOTween.Sequence();
        inGameSequence.Append(menuPanel.transform.DOScale(new Vector2(0f, 0f), 1.5f).SetEase(Ease.InOutCubic))
            .OnComplete(uiManager.OnPlayGame);
    }
    public void OpenShopTween(float duration)
    {
        //Click ShopBtn
        foreach (var image in mainImage)
        {
            image.DOFade(0f, duration);
        }
        purchasedImage.DOFade(0, duration);
    }
    public void CloseShopTween(float duration)
    {
        //Click ShopBtn
        foreach (var image in mainImage)
        {
            image.DOFade(1f, duration);
        }
        purchasedImage.DOFade(0.5f, duration);
    }
    public void OpenInfoTween()
    {
        Color tempColor = new Color(infoImage[1].color.r, infoImage[1].color.g, infoImage[1].color.b, 0);
        infoImage[1].color = tempColor;
        infoText[0].color = tempColor;
        Sequence infoSequence = DOTween.Sequence();
        infoSequence.Append(infoPanel.gameObject.GetComponent<Image>().DOFade(1, 1.5f))
            .OnComplete(() => infoTween(1, 2));
    }
    public void CloseInFoTween()
    {
        UiManager uiManager = FindObjectOfType<UiManager>();
        Sequence infoSequence = DOTween.Sequence();
        infoSequence.AppendCallback(() => infoTween(0, 1))
            .Append(infoPanel.gameObject.GetComponent<Image>().DOFade(0, 2f))
            .OnComplete(uiManager.OnPanelInfo);

    }

    void infoTween(float endValue, float duration)
    {
        foreach (var text in infoText)
        {
            text.DOFade(endValue, duration);
        }
        foreach (var image in infoImage)
        {
            image.DOFade(endValue, duration);
        }
    }

    public void AdsTween()
    {
        adsPanel.localScale = new Vector2(0, 0);
        noThanks.interactable = false;
        noThxText.color = new Color(noThxText.color.r, noThxText.color.g, noThxText.color.b, 0);
        adsPanel.transform.DOScale(new Vector2(1f, 1f), 2f).SetEase(Ease.InOutCubic)
            .OnComplete(showNothank);
    }
    void showNothank() 
    {
        noThxText.DOFade(1, 2).OnComplete(() => noThanks.interactable = true);
    }
    public void resultTween(float endValue, float duration) 
    {
        foreach (var text in resultText)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            text.DOFade(endValue, duration);
        }
        foreach (var image in resultImage)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
            image.DOFade(endValue, duration);
        }
    }
 

}
