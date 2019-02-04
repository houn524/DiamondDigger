using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScaleManager : MonoBehaviour {

    public static SpriteScaleManager instance = null;

    private float screenWidth;
    private float screenHeight;
    public float ScreenHeight {
        get {
            return screenHeight;
        }
    }

    private Vector3 leftDown;
    public Vector3 LeftDown {
        get {
            return leftDown;
        }
    }
    private Vector3 rightUpper;

    private float calWidth;
    public float CalWidth {
        get {
            return calWidth;
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

    // Use this for initialization
    void Start () {
        leftDown = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        rightUpper = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        screenWidth = rightUpper.x - leftDown.x;
        screenHeight = rightUpper.y - leftDown.y;

        calWidth = screenWidth / 7;
	}
}