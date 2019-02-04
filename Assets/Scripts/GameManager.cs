using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public const float MAXCOMBOTIME = 3.0f;

    public GameObject gameOverPanel;
    public AudioSource TimerSoundSource;
    public float maxLimitTime = 15.0f;
    public float limitTimeCoef = 1.0f;
    public float maxLimitTimeCoef = 10.0f;

    private bool isGameStart = false;
    public bool IsGameStart {
        set {
            isGameStart = value;
        } get {
            return isGameStart;
        }
    }

    private bool isGameOver = false;
    public bool IsGameOver {
        get {
            return isGameOver;
        } set {
            isGameOver = value;
        }
    }

    private int score = 0;
    public int Score {
        get {
            return score;
        }
        set {
            score = value;
        }
    }

    private float feverProgress = 0f;
    public float FeverProgress {
        get {
            return feverProgress;
        } set {
            feverProgress = value;
        }
    }

    private int combo = 0;
    public int Combo {
        get {
            return combo;
        }
    }

    private int maxCombo = 0;
    public int MaxCombo {
        get {
            return maxCombo;
        }
    }

    public float limitTime;

    private float maxComboTime = MAXCOMBOTIME;
    private float comboTime = 0.0f;

    private bool isFeverMode = false;
    public bool IsFeverMode {
        get {
            return isFeverMode;
        }
    }

    void Awake() {
        if(instance == null) {
            instance = this;
        } else if(instance != null) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Update() {
        if (!isGameStart)
            return;

        limitTimeCoef = Mathf.Min(1.0f + score / 50000, maxLimitTimeCoef);
        limitTime -= Time.deltaTime * limitTimeCoef;
        if (limitTime <= 0f && isGameOver == false) {
            StartCoroutine(GameOver());
        } else if(limitTime <= 15.0f && isGameOver == false && TimerSoundSource.isPlaying == false) {
            TimerSoundSource.Play();
        } else if(limitTime > 15.0f && isGameOver == false && TimerSoundSource.isPlaying) {
            TimerSoundSource.Stop();
        }

        comboTime += Time.deltaTime;
        if(comboTime > maxComboTime) {
            resetCombo();
        }
    }

    public void GameStart() {
        //UnityAdsManager.instance.PlayCount++;
        IsGameStart = true;
    }

    IEnumerator GameOver() {
        TimerSoundSource.Stop();
        GameObject.Find("BackgroundMusicManager").GetComponent<AudioSource>().Stop();
        GameObject.Find("GameOverSoundManager").GetComponent<AudioSource>().Play();
        gameOverPanel.SetActive(true);
        gameOverPanel.transform.Find("Panel/txtGameOver").GetComponent<Animator>().SetTrigger("trigGameOver");
        isGameOver = true;

        yield return new WaitForSeconds(2.0f);

        //if (UnityAdsManager.instance.PlayCount >= 3)
        //    UnityAdsManager.instance.ShowSimpleAd();
        //else 
            SceneManager.LoadScene("ResultScene");
    }

    public void hitCombo() {
        combo++;
        if(combo > maxCombo) {
            maxCombo = combo;
        }
        
        maxComboTime = Mathf.Max(maxComboTime - 0.1f, 2.0f);

        comboTime = 0.0f;
    }

    public void resetCombo() {
        combo = 0;
        maxComboTime = MAXCOMBOTIME;
        feverProgress = 0.0f;
        isFeverMode = false;
    }

    public void FeverMode() {
        GameObject.Find("FeverSoundManager").GetComponent<AudioSource>().Play();
        isFeverMode = true;
    }
}
