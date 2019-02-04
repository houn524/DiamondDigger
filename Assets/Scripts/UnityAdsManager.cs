using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class UnityAdsManager : MonoBehaviour {

    public static UnityAdsManager instance = null;

    private int playCount = 0;
    public int PlayCount {
        set {
            if(value >= 4) {
                playCount = 1;
            } else {
                playCount = value;
            }
        }
        get {
            return playCount;
        }
    }

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != null) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void ShowSimpleAd() {
        if(Advertisement.IsReady()) {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show(options);
        }
    }

    private void HandleShowResult(ShowResult result) {
        switch(result) {
            case ShowResult.Finished:
                break;
            case ShowResult.Skipped:
                break;
            case ShowResult.Failed:
                break;
        }
        SceneManager.LoadScene("ResultScene");
    }
}
