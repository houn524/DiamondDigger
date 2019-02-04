using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboText : MonoBehaviour {

    private int prevCombo;
    private float fadeOutTime = 0.5f;
    private float timeSpan = 0.0f;

	// Use this for initialization
	void Start () {
        prevCombo = GameManager.instance.Combo;
	}
	
	// Update is called once per frame
	void Update () {
        timeSpan += Time.deltaTime;

        if(prevCombo != GameManager.instance.Combo) {
            prevCombo = GameManager.instance.Combo;

            GetComponent<Animator>().SetBool("bFadeOut", false);
            if (prevCombo <= 0) {
                GetComponent<Text>().text = "";
            } else {
                GetComponent<Text>().text = prevCombo + " Combo";
            }

            timeSpan = 0.0f;
        }

        if(timeSpan > fadeOutTime) {
            GetComponent<Animator>().SetBool("bFadeOut", true);
            timeSpan = 0.0f;
        }
	}
}
