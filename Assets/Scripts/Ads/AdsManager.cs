using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdsManager : MonoBehaviour
{
#if UNITY_ANDROID
    string App_ID = "ca-app-pub-2766444901440139~6253473690";
    string Banner_Ad_ID = "ca-app-pub-2766444901440139/8857238696";
    string Interstitial_Ad_ID = "ca-app-pub-2766444901440139/8011060889";
    string Video_Ad_ID = "ca-app-pub-2766444901440139/8151689351";
#elif UNITY_IPHONE
        string App_ID = "";
        string Banner_Ad_ID = "";
        string Interstitial_Ad_ID = "";
        string Video_Ad_ID = "";
#else
                string adUnitId = "unexpected_platform";
#endif

    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardedAd gameOverRewardedAds;
   
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        //สร้าง banner
        this.RequestBanner();
        this.RequestIntesstitial();
        
        gameOverRewardedAds = CreateAndLoadRewardedAds(Video_Ad_ID);
    }
    public void showBannerAds() 
    {
        if (PlayerPrefs.HasKey("ads") == false)
        {
            AdRequest request = new AdRequest.Builder().Build();
            this.bannerView.LoadAd(request);
        }
    }
    private void RequestBanner()
    {
        this.bannerView = new BannerView(Banner_Ad_ID, AdSize.Banner, AdPosition.Bottom);
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
    }
    public void showInterstitialAds() 
    {
        if (this.interstitial.IsLoaded())
        {
            if (PlayerPrefs.HasKey("ads") == false)
            {
                this.interstitial.Show();
            }
        }

    }
    private void RequestIntesstitial() 
    {
        this.interstitial = new InterstitialAd(Interstitial_Ad_ID);
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;

        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }
    private RewardedAd CreateAndLoadRewardedAds(string adUnitID) 
    {
        RewardedAd rewardedAd = new RewardedAd(adUnitID);
        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
        return rewardedAd;

    }
    public void UserChoseToWatchGameOverRewardAds() // run GameOver Reward Ads
    {
        if (gameOverRewardedAds.IsLoaded())
        {
            gameOverRewardedAds.Show();
        }
    }

    public void removeAds() 
    {
        bannerView.Destroy();
        interstitial.Destroy();
    }
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("Ads Loaded");
    }
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("Ads Failed to Load");
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        Debug.Log("RewardedAdOpening");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        Debug.Log("HandleRewardedAdFailedToShow");
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        Debug.Log("RewardedAdClosed");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        Debug.Log("Revive");
        GameSetting.gamesettingInstance.rewardWhenPlayerWatch();
    }

}
