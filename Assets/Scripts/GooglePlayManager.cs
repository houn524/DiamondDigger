using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GooglePlayManager : MonoBehaviour {

    public static GooglePlayManager instance = null;

    public bool isSigningIn = false;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != null) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {

#if UNITY_ANDROID

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .Build();

        PlayGamesPlatform.InitializeInstance(config);

        PlayGamesPlatform.DebugLogEnabled = true;

        PlayGamesPlatform.Activate();

#elif UNITY_IOS

        GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);

#endif

    }

    public void SignIn() {
        Debug.Log("SignIn!!");
        Social.localUser.Authenticate((bool success) => {
            if(success) {
                MainMenuManager mainMenuManager = GameObject.Find("MainMenuManager").GetComponent<MainMenuManager>();
                mainMenuManager.LogIn();
            } else {
                isSigningIn = false;
                MainMenuManager mainMenuManager = GameObject.Find("MainMenuManager").GetComponent<MainMenuManager>();
                mainMenuManager.LogInFailed();
            }
        });

        isSigningIn = true;
    }

    public void SignOut() {
        PlayGamesPlatform.Instance.SignOut();
    }

//    public void UnlockAchievement(int score) {
//        if(score >= 100) {
//#if UNITY_ANDROID
//            PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement, 100f, null);
//#elif UNITY_IOS
//            Social.ReportProgress("Score", 100f, null);
//#endif
//        }
//    }

    public void ShowAchievementUI() {
        if(Social.localUser.authenticated == false) {
            Social.localUser.Authenticate((bool success) => {
                if (success) {
                    Social.ShowAchievementsUI();
                    return;
                } else {
                    return;
                }
            });
        }

        Social.ShowAchievementsUI();
    }

    public void ReportScore(int score) {

#if UNITY_ANDROID

        PlayGamesPlatform.Instance.ReportScore(score, GPGSIds.leaderboard_score, (bool success) => {
            if (success) {

            } else {

            }
        });

#elif UNITY_IOS

        Social.ReportScore(score, "Leaderboard_ID", (bool success) => {
            if(success) {

            } else {

            }
        });

#endif

    }

    public void ShowLeaderboardUI() {
        
        if(Social.localUser.authenticated == false) {
            Social.localUser.Authenticate((bool success) => {
                if (success) {
                    PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_score);
                    return;
                } else {
                    return;
                }
            });
        }

#if UNITY_ANDROID
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_score);
#elif UNITY_IOS
        GameCenterPlatform.ShowLeaderboardUI("Leaderboard_ID", UnityEngine.SocialPlatforms.TimeScope.AllTime);
#endif

    }

    public bool isLoggedIn() {
        return Social.localUser.authenticated;
    }

    public int GetScore() {
        IScore score = null;

        PlayGamesPlatform.Instance.LoadScores(
            GPGSIds.leaderboard_score,
            LeaderboardStart.PlayerCentered,
            1,
            LeaderboardCollection.Public,
            LeaderboardTimeSpan.AllTime,
            (LeaderboardScoreData data) => {
                score = data.PlayerScore;
            });

        return (int)(score.value);
    }

}
