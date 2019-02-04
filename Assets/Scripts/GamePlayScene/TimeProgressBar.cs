using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeProgressBar : MonoBehaviour {

    private Image image;

    // Use this for initialization
    void Start() {
        image = GetComponent<Image>();
        image.fillAmount = 1f;
    }

    // Update is called once per frame
    void Update() {
        image.fillAmount = GameManager.instance.limitTime / GameManager.instance.maxLimitTime;
    }
}
