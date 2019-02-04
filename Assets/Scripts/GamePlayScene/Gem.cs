using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Directions {
    LEFT,
    RIGHT,
    UP,
    DOWN,
    NONE
}

public class Gem : MonoBehaviour {

    public float downSpeed = 3.0f;

    public GemSpawnManager gemSpawnManager;
    public DigSoundManager digSoundSource;

    private int x = 0;
    private int y = 0;
    private int gemType = 0;
    private bool isProcessed = false;
    public bool isMoving = true;
    public int score = 100;
    public float feverProgress = 0.01f;

    private float bottomY = -3.85f;
    public float BottomY {
        set {
            bottomY = value;
        }
    }
    private float yGap = 0.8f;
    public float YGap {
        set {
            yGap = value;
        }
    }

    private Object hitGemObj;
    public Object HitGemObj {
        set {
            hitGemObj = value;
        }
    }

    void Start() {
        //float gemSize = GetComponent<SpriteRenderer>().bounds.size.y;

        gemSpawnManager = GameObject.Find("SpawnPos").GetComponent<GemSpawnManager>();
        digSoundSource = GameObject.Find("DigSoundManager").GetComponent<DigSoundManager>();

        //yGap = yGap * (SpriteScaleManager.instance.CalWidth / yGap);
        //bottomY = SpriteScaleManager.instance.LeftDown.y + (gemSize / 2)
        //    + (SpriteScaleManager.instance.ScreenHeight * 0.08f);

        //GameObject gemMask = GameObject.Find("GemMask");

        //gemMask.transform.position = new Vector3(0, bottomY + (gemSize * 6) + (gemSize / 2) + (gemMask.GetComponent<SpriteMask>().bounds.size.y / 2), 1);
    }

    void Update() {
        if (transform.position.y > (bottomY + (y * yGap))) {
            gemSpawnManager.isMoving = true;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, (bottomY + (y * yGap))),
                Time.deltaTime * downSpeed);
        } else {
            gemSpawnManager.isMoving = false;
        }
    }

    void OnMouseDown() {
        if (gemSpawnManager.isMoving || GameManager.instance.IsGameOver || !GameManager.instance.IsGameStart) {
            return;
        }
        gemSpawnManager.currentProcessedObj.Clear();
        processGem();

        if(gemSpawnManager.currentProcessedObj.Count >= 3) {
            if (GameManager.instance.IsFeverMode)
                digSoundSource.PlayDigDiamondFever();
            else
                digSoundSource.PlayDigDiamond();

            foreach (GameObject obj in gemSpawnManager.currentProcessedObj) {
                Gem gem = obj.GetComponent<Gem>();
                gemSpawnManager.currentGemsNum[gem.x]--;
                gemSpawnManager.currentGems[gem.x, gem.y] = null;
                Instantiate(hitGemObj, obj.transform.position, obj.transform.rotation);

                Destroy(obj);

                int gemScore = score * Mathf.Max(GameManager.instance.Combo, 1);

                if (GameManager.instance.IsFeverMode)
                    gemScore *= 2;

                GameManager.instance.Score += gemScore;
                GameManager.instance.FeverProgress += feverProgress;
            }

            gemSpawnManager.PullAll();
            gemSpawnManager.SpawnAll();
            GameManager.instance.hitCombo();
            GameManager.instance.limitTime = Mathf.Min(GameManager.instance.limitTime + 5f, 30.0f);
        }
        else {
            digSoundSource.PlayDigRock();

            foreach (GameObject obj in gemSpawnManager.currentProcessedObj) {
                obj.GetComponent<Gem>().isProcessed = false;
                obj.GetComponent<Animator>().SetTrigger("trigGemFail");
                GameManager.instance.FeverProgress = 0f;
            }

            GameManager.instance.resetCombo();
            GameManager.instance.limitTime -= 5.0f;
        }
    }

    public void processGem() {
        isProcessed = true;
        gemSpawnManager.currentProcessedObj.Add(gameObject);
        if (x > 0 && gemSpawnManager.currentGems[x - 1, y] != null && gemSpawnManager.currentGems[x - 1, y].isProcessed == false &&
            gemSpawnManager.currentGems[x - 1, y].gemType == gemType) {
            gemSpawnManager.currentGems[x - 1, y].processGem();
        }
        if (x < 6 && gemSpawnManager.currentGems[x + 1, y] != null && gemSpawnManager.currentGems[x + 1, y].isProcessed == false &&
            gemSpawnManager.currentGems[x + 1, y].gemType == gemType) {
            gemSpawnManager.currentGems[x + 1, y].processGem();
        }
        if (y < 6 && gemSpawnManager.currentGems[x, y + 1] != null && gemSpawnManager.currentGems[x, y + 1].isProcessed == false &&
            gemSpawnManager.currentGems[x, y + 1].gemType == gemType) {
            gemSpawnManager.currentGems[x, y + 1].processGem();
        }
        if (y > 0 && gemSpawnManager.currentGems[x, y - 1] != null && gemSpawnManager.currentGems[x, y - 1].isProcessed == false &&
            gemSpawnManager.currentGems[x, y - 1].gemType == gemType) {
            gemSpawnManager.currentGems[x, y - 1].processGem();
        }
    }

    public void Touch() {
        processGem();
    }

    public void SetXY(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public void SetGemType(int gemType) {
        this.gemType = gemType;
    }

    public int GetX() { return this.x; }
    public int GetY() { return this.y; }
}
