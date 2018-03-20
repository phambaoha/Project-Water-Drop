using GoogleMobileAds.Api;
using System;
using UnityEngine.Advertisements;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoogleMobileAdsDemoHandler : IDefaultInAppPurchaseProcessor
{
    private readonly string[] validSkus = { "android.test.purchased" };

    // Will only be sent on a success.
    public void ProcessCompletedInAppPurchase(IInAppPurchaseResult result)
    {
        result.FinishPurchase();
        ShareRateAds.OutputMessage = "Purchase Succeeded! Credit user here.";
    }

    // Check SKU against valid SKUs.

    public bool IsValidPurchase(string sku)
    {
        foreach (string validSku in this.validSkus)
        {
            if (sku == validSku)
            {
                return true;
            }
        }

        return false;
    }

    // Return the app's public key.
    // oke
    public string AndroidPublicKey
    {
        // In a real app, return public key instead of null.
        get { return null; }
    }
}

// Example script showing how to invoke the Google Mobile Ads Unity plugin.
public class ShareRateAds : MonoBehaviour
{
    public static ShareRateAds shareRateAds;

    //Banner
    public string idBannerAndroid;
    public string idBannerIOS;
    public AdPosition positionBaner;
    public bool banner;
    //Interstitial
    public string idIntersAndroid;
    public string idIntersIOS;
    public int countShow;
    public bool inters;
    //RewardBasedVideo
    public string idRewardAndroid;
    public string idRewardIOS;
    public bool reward;
    //Share Rate
    public string link;

    [SerializeField]
    string IdForIos;
    [SerializeField]
    string IdForAndroid;

    string zone = "";
    private bool check;

    [HideInInspector]
    public int count = 0;
    private BannerView bannerView;
    private InterstitialAd interstitial;
    private InterstitialAd interstitial2;
    private NativeExpressAdView nativeExpressAdView;
    private RewardBasedVideoAd rewardBasedVideo;
    private float deltaTime = 0.0f;
    private static string outputMessage = string.Empty;

    public static string OutputMessage
    {
        set { outputMessage = value; }
    }

    public void Awake()
    {
        if (shareRateAds == null)
        {
            DontDestroyOnLoad(gameObject);
            shareRateAds = this;
        }
        else if (shareRateAds != this)
        {
            Destroy(gameObject);
        }


    }
    public void Start()
    {
        if (reward)
        {
            RequestRewardBasedVideo();
        }
        if (banner)
            this.RequestBanner();
        if (inters)
        {
            this.RequestInterstitial();
            this.RequestInterstitial2();
        }
    }
    //goi show video
    public void ShowBasedVideo()
    {
#if UNITY_EDITOR

#elif UNITY_ANDROID || UNITY_IOS
        ShowOptions options = new ShowOptions();
        options.resultCallback = AdCallBackHandler;
        if (Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo", options);
        }
        
#endif
    }
#if UNITY_EDITOR

#elif UNITY_ANDROID || UNITY_IOS
    void AdCallBackHandler(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                {
                    rewardForWatchFullVideo();
                    break;
                }
        }

    }
#endif
    public void rewardForWatchFullVideo()
    {
        PlayerPrefs.SetInt("watchfullvideo", 1);
        SoundManager.soundManager.music.mute = false;
        SceneManager.LoadScene(2);
    }

    public void Update()
    {
        // Calculate simple moving average for time to render screen. 0.1 factor used as smoothing
        // value.
        this.deltaTime += (Time.deltaTime - this.deltaTime) * 0.1f;

    }
    public void HideBaner()
    {
        this.bannerView.Hide();
    }
    public void ShowBaner()
    {
        this.bannerView.Show();
    }
    public void ShowIn()
    {
        count++;
        if (count >= countShow)
        {
            count = 0;
            this.ShowInterstitial();
        }
    }
    public void Share()
    {
        Share(link, "", "");
    }
    public void Rate()
    {
        Application.OpenURL(link);
    }
    public void Share(string shareText, string imagePath, string url, string subject = "")
    {
#if UNITY_ANDROID
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + imagePath);
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
        intentObject.Call<AndroidJavaObject>("setType", "image/png");

        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareText);

        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, subject);
        currentActivity.Call("startActivity", jChooser);
#elif UNITY_IOS
        CallSocialShareAdvanced(shareText, subject, url, imagePath);
#else
		Debug.Log("No sharing set up for this platform.");
#endif
    }
#if UNITY_IOS
    public struct ConfigStruct
    {
        public string title;
        public string message;
    }

    [DllImport("__Internal")] private static extern void showAlertMessage(ref ConfigStruct conf);

    public struct SocialSharingStruct
    {
        public string text;
        public string url;
        public string image;
        public string subject;
    }

    [DllImport("__Internal")] private static extern void showSocialSharing(ref SocialSharingStruct conf);

    public static void CallSocialShare(string title, string message)
    {
        ConfigStruct conf = new ConfigStruct();
        conf.title = title;
        conf.message = message;
        showAlertMessage(ref conf);
    }


    public static void CallSocialShareAdvanced(string defaultTxt, string subject, string url, string img)
    {
        SocialSharingStruct conf = new SocialSharingStruct();
        conf.text = defaultTxt;
        conf.url = url;
        conf.image = img;
        conf.subject = subject;

        showSocialSharing(ref conf);
    }
#endif
    // Returns an ad request with custom ad targeting.
    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            .AddTestDevice(AdRequest.TestDeviceSimulator)
            .AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
            .AddKeyword("game")
            .SetGender(Gender.Male)
            .SetBirthday(new DateTime(1985, 1, 1))
            .TagForChildDirectedTreatment(false)
            .AddExtra("color_bg", "9B30FF")
            .Build();
    }

    public void RequestBanner()
    {
        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = idBannerAndroid;
#elif UNITY_IPHONE || UNITY_IOS
        string adUnitId = idBannerIOS;
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.SmartBanner, positionBaner);

        // Register for ad events.
        this.bannerView.OnAdLoaded += this.HandleAdLoaded;
        this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        this.bannerView.OnAdLoaded += this.HandleAdOpened;
        this.bannerView.OnAdClosed += this.HandleAdClosed;
        this.bannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;

        // Load a banner ad.
        this.bannerView.LoadAd(this.CreateAdRequest());
    }

    public void RequestInterstitial()
    {
        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = idIntersAndroid;
#elif UNITY_IPHONE || UNITY_IOS
        string adUnitId = idIntersIOS;
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create an interstitial.
        this.interstitial = new InterstitialAd(adUnitId);

        // Register for ad events.
        this.interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
        this.interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
        this.interstitial.OnAdOpening += this.HandleInterstitialOpened;
        this.interstitial.OnAdClosed += this.HandleInterstitialClosed;
        this.interstitial.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;

        // Load an interstitial ad.
        this.interstitial.LoadAd(this.CreateAdRequest());
    }
    public void RequestInterstitial2()
    {
        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = idIntersAndroid;
#elif UNITY_IPHONE || UNITY_IOS
        string adUnitId = idIntersIOS;
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create an interstitial.
        interstitial2 = new InterstitialAd(adUnitId);
        // Register for ad events.
        interstitial2.OnAdLoaded += this.HandleInterstitialLoaded;
        interstitial2.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
        interstitial2.OnAdOpening += this.HandleInterstitialOpened;
        interstitial2.OnAdClosed += this.HandleInterstitialClosed;
        interstitial2.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;

        // Load an interstitial ad.
        interstitial2.LoadAd(this.CreateAdRequest());
    }
    private void RequestNativeExpressAdView()
    {
        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1072772517";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/2562852117";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create a 320x150 native express ad at the top of the screen.
        this.nativeExpressAdView = new NativeExpressAdView(
            adUnitId,
            new AdSize(320, 150),
            AdPosition.Top);

        // Register for ad events.
        this.nativeExpressAdView.OnAdLoaded += this.HandleNativeExpressAdLoaded;
        this.nativeExpressAdView.OnAdFailedToLoad += this.HandleNativeExpresseAdFailedToLoad;
        this.nativeExpressAdView.OnAdLoaded += this.HandleNativeExpressAdOpened;
        this.nativeExpressAdView.OnAdClosed += this.HandleNativeExpressAdClosed;
        this.nativeExpressAdView.OnAdLeavingApplication += this.HandleNativeExpressAdLeftApplication;

        // Load a native express ad.
        this.nativeExpressAdView.LoadAd(this.CreateAdRequest());
    }

    private void RequestRewardBasedVideo()
    {
        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR

#elif UNITY_ANDROID
        Advertisement.Initialize(IdForAndroid, true);
#elif UNITY_IPHONE
        Advertisement.Initialize(IdForIos, true);
#endif

    }

    public void ShowInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
            this.RequestInterstitial();
        }
        else
        {
            interstitial2.Show();
            RequestInterstitial2();
        }
    }

    public void ShowRewardBasedVideo()
    {
        if (this.rewardBasedVideo.IsLoaded())
        {
            this.rewardBasedVideo.Show();
            RequestRewardBasedVideo();
        }
        else
        {
            MonoBehaviour.print("Reward based video ad is not ready yet");
        }
    }

    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeftApplication event received");
    }

    #endregion

    #region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialLoaded event received");
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    public void HandleInterstitialOpened(object sender, EventArgs args)
    {
#if UNITY_IPHONE
        Time.timeScale = 0;
#endif
    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
#if UNITY_IPHONE
        Time.timeScale = 1;
#endif
    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialLeftApplication event received");
    }

    #endregion

    #region Native express ad callback handlers

    public void HandleNativeExpressAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleNativeExpressAdAdLoaded event received");
    }

    public void HandleNativeExpresseAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleNativeExpressAdFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleNativeExpressAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleNativeExpressAdAdOpened event received");
    }

    public void HandleNativeExpressAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleNativeExpressAdAdClosed event received");
    }

    public void HandleNativeExpressAdLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleNativeExpressAdAdLeftApplication event received");
    }

    #endregion

    #region RewardBasedVideo callback handlers

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {

    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        if (args.Amount == 1)
        {
            check = true;
        }
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }

    #endregion
}
