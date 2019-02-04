using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class MainMenuManager : MonoBehaviour {

    public Text txtStart;
    public Button btnStart;
    public Button btnRanking;

    private const float BACK_KEY_INTERVAL = 2.0f;
    private float backKeyPressedTime = 0f;
    private bool backKeyPressed = false;

    void Start() {
        txtStart.text = "Start";
        btnStart.interactable = true;
        btnRanking.interactable = true;
        //#if UNITY_EDITOR
        //        SceneManager.LoadScene("GamePlayScene");
        //#endif

        //        if (GooglePlayManager.instance.isLoggedIn()) {
        //            LogIn();
        //        } else {
        //            GooglePlayManager.instance.SignIn();
        //        }
    }

    void Update() {
        if(Application.platform == RuntimePlatform.Android) {
            if(Input.GetKeyDown(KeyCode.Escape)) {
                if(backKeyPressed && backKeyPressedTime < BACK_KEY_INTERVAL) {
                    Debug.Log("Quit!");
                    Application.Quit();
                } else {
                    Debug.Log("Pressed!");
                    backKeyPressed = true;
                    backKeyPressedTime = 0f;
                }
            }

            if(backKeyPressed) {
                Debug.Log(backKeyPressedTime);
                backKeyPressedTime += Time.deltaTime;
                if(backKeyPressedTime >= BACK_KEY_INTERVAL) {
                    backKeyPressed = false;
                }
            }
        }

        //if(GooglePlayManager.instance.isSigningIn) {
        //    btnStart.interactable = false;
        //    txtStart.text = "Sign In...";
        //} else if(!GooglePlayManager.instance.isLoggedIn()) {
        //    txtStart.text = "Sign In";
        //    btnRanking.interactable = false;
        //}
    }

	public void StartGame() {
        SceneManager.LoadScene("GamePlayScene");
        //if(!GooglePlayManager.instance.isLoggedIn()) {
        //    GooglePlayManager.instance.SignIn();
        //} else {
        //    SceneManager.LoadScene("GamePlayScene");
        //}
    }

    public void Quit() {
        Application.Quit();
    }

    public void ShowRanking() {
        GooglePlayManager.instance.ShowLeaderboardUI();
    }

    public void LogIn() {
        GooglePlayManager.instance.isSigningIn = false;
        txtStart.text = "Start";
        btnStart.interactable = true;
        btnRanking.interactable = true;

        PlayGamesPlatform.Instance.LoadScores(
            GPGSIds.leaderboard_score,
            LeaderboardStart.PlayerCentered,
            1,
            LeaderboardCollection.Public,
            LeaderboardTimeSpan.AllTime,
            (LeaderboardScoreData data) => {
                Debug.Log(data.Valid);
                Debug.Log(data.Id);
                Debug.Log(data.PlayerScore);
                Debug.Log(data.PlayerScore.userID);
                Debug.Log(data.PlayerScore.formattedValue);
            });

    }

    public void LogInFailed() {
        btnStart.interactable = true;
    }
}
