using UnityEngine;
using GoogleMobileAds.Api;
using System;
using System.Collections;

public class RewardAd : MonoBehaviour
{
    private RewardedAd rewardedAd;

    public Player player;
    public GameObject loading;

    public int Type;

    private void Start()
    {
        MobileAds.Initialize(initStatus => { });
    }

    public void CreateAndLoadRewardedAd()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-1864928959429416/3138986110";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-1864928959429416/9135141148";
#else
        string adUnitId = "unexpected_platform";
#endif

        // 이전 광고 객체 정리
        if (rewardedAd != null) {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        AdRequest request = new AdRequest();

        RewardedAd.Load(adUnitId, request, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null) {
                Debug.LogError("Rewarded ad failed to load: " + error);
                loading.SetActive(false);
                Time.timeScale = 1f;
                AudioListener.volume = 1f;
                return;
            }

            Debug.Log("Rewarded ad loaded.");
            rewardedAd = ad;

            RegisterEventHandlers(rewardedAd);
        });
    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
        ad.OnAdPaid += adValue =>
        {
            Debug.Log($"Rewarded ad paid {adValue.Value} {adValue.CurrencyCode}");
        };

        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad impression recorded.");
        };

        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad clicked.");
        };

        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen opened.");
        };

        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen closed.");
            loading.SetActive(false);
            Time.timeScale = 1f;
            AudioListener.volume = 1f;

            // 닫힌 뒤 다음 광고 미리 로드하고 싶으면 여기서 호출
            // CreateAndLoadRewardedAd();
        };

        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content: " + error);
            loading.SetActive(false);
            Time.timeScale = 1f;
            AudioListener.volume = 1f;
        };
    }

    public void UserChoseToWatchAd(int type)
    {
        Type = type;
        loading.SetActive(true);

        CreateAndLoadRewardedAd();
        StartCoroutine(ShowRewardedAdWhenReady());
    }

    private IEnumerator ShowRewardedAdWhenReady()
    {
        float timeout = 10f;
        float elapsed = 0f;

        while ((rewardedAd == null || !rewardedAd.CanShowAd()) && elapsed < timeout) {
            yield return new WaitForSeconds(0.2f);
            elapsed += 0.2f;
        }

        loading.SetActive(false);

        if (rewardedAd != null && rewardedAd.CanShowAd()) {
            Time.timeScale = 0f;
            AudioListener.volume = 0f;

            rewardedAd.Show((Reward reward) =>
            {
                Debug.Log($"Reward earned: {reward.Amount} {reward.Type}");

                switch (Type) {
                    case 1:
                        player.diamond += 15;
                        break;
                    case 2:
                        player.minerclone.adsEarn();
                        break;
                }

                AudioListener.volume = 1f;
                Time.timeScale = 1f;
            });
        } else {
            Debug.LogWarning("Rewarded ad is not ready.");
            Time.timeScale = 1f;
            AudioListener.volume = 1f;
        }
    }

    private void OnDestroy()
    {
        if (rewardedAd != null) {
            rewardedAd.Destroy();
            rewardedAd = null;
        }
    }
}