using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class ResultManager : MonoBehaviour {

    public GameObject imgNewScore;
    public GameObject imgNewScoreLight;

    void Start() {
        //PlayGamesPlatform.Instance.LoadScores(
        //    GPGSIds.leaderboard_score,
        //    LeaderboardStart.PlayerCentered,
        //    1,
        //    LeaderboardCollection.Public,
        //    LeaderboardTimeSpan.AllTime,
        //    (LeaderboardScoreData data) => {
        //        CheckNewScore((int)data.PlayerScore.value);
        //    });
    }

    public void CheckNewScore(int score) {
        if(GameManager.instance.Score > score) {
            imgNewScore.SetActive(true);
            imgNewScoreLight.SetActive(true);
            Debug.Log("New Score!!");
        }

        GooglePlayManager.instance.ReportScore(GameManager.instance.Score);
        Debug.Log("ReportScore!!");
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenuScene");
        Destroy(GameManager.instance.gameObject);
    }

    public void Replay() {
        SceneManager.LoadScene("GamePlayScene");
        Destroy(GameManager.instance.gameObject);
    }
}
