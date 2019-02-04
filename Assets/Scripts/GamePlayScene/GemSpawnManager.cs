using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawnManager : MonoBehaviour {

    public Gem gem;
    public Sprite[] gemSprites;
    public int fourGemCombo = 50;
    public Object[] hitGemObj;

    public  Transform[] spawnPoses = new Transform[7];

    public int[] currentGemsNum = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
    public Gem[,] currentGems = new Gem[7, 7];
    public int currentProcessedNum = 0;
    public List<GameObject> currentProcessedObj = new List<GameObject>();

    public bool isMoving = false;

    private float bottomY = 0f;
    private float yGap = 0.8f;

    private bool spawnSwitch = true;

    // Use this for initialization
    void Start () {
        float distance = SpriteScaleManager.instance.CalWidth;
        float newPos = distance * -3;

        float gemSize = SpriteScaleManager.instance.CalWidth;

        bottomY = SpriteScaleManager.instance.LeftDown.y + (gemSize / 2)
            + (SpriteScaleManager.instance.ScreenHeight * 0.08f);

        yGap = yGap * (SpriteScaleManager.instance.CalWidth / yGap);

        foreach (Transform pos in spawnPoses) {
            pos.position = new Vector3(newPos, bottomY + (distance * 7), 1);

            newPos += distance;
        }

        GameObject gemMask = GameObject.Find("GemMask");

        gemMask.transform.position = new Vector3(0, bottomY + (gemSize * 6) + (gemSize / 2) + (gemMask.GetComponent<SpriteMask>().bounds.size.y / 2), 1);

        StartCoroutine(SpawnGem(0));
        StartCoroutine(SpawnGem(1));
        StartCoroutine(SpawnGem(2));
        StartCoroutine(SpawnGem(3));
        StartCoroutine(SpawnGem(4));
        StartCoroutine(SpawnGem(5));
        StartCoroutine(SpawnGem(6));
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void SpawnAll() {
        StartCoroutine(SpawnGem(0));
        StartCoroutine(SpawnGem(1));
        StartCoroutine(SpawnGem(2));
        StartCoroutine(SpawnGem(3));
        StartCoroutine(SpawnGem(4));
        StartCoroutine(SpawnGem(5));
        StartCoroutine(SpawnGem(6));
    }

    public void PullAll() {
        PullGem(0);
        PullGem(1);
        PullGem(2);
        PullGem(3);
        PullGem(4);
        PullGem(5);
        PullGem(6);
    }

    public void PullGem(int pos) {
        int top = 0;

        for(int i = 0; i < 7; i++) {
            if (currentGems[pos, i] != null) {
                currentGems[pos, i].SetXY(pos, top);
                currentGems[pos, top] = currentGems[pos, i];
                top++;
            }
        }
    }

    public IEnumerator SpawnGem(int pos) {
        if (currentGemsNum[pos] < 7) {
            int minRange = 0;
            int maxRange = 3;

            int gemType = Random.Range(minRange, maxRange);
            Gem NewGem = Instantiate(gem, spawnPoses[pos].position, spawnPoses[pos].rotation).GetComponent<Gem>();
            NewGem.GetComponent<SpriteRenderer>().sprite = gemSprites[gemType];
            NewGem.BottomY = bottomY;
            NewGem.YGap = yGap;
            NewGem.HitGemObj = hitGemObj[gemType];
            NewGem.SetXY(pos, currentGemsNum[pos]);
            NewGem.SetGemType(gemType);
            Vector3 objSize = NewGem.GetComponent<SpriteRenderer>().bounds.size;
            NewGem.transform.localScale = new Vector3(SpriteScaleManager.instance.CalWidth / objSize.x,
                SpriteScaleManager.instance.CalWidth / objSize.y, 1);
            currentGems[pos, currentGemsNum[pos]] = NewGem;
            currentGemsNum[pos]++;

            yield return new WaitForSeconds(0.02f);

            StartCoroutine(SpawnGem(pos));
        }
    }
}
