using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

    private Text textComp;

    private int prevScore = 0;
    private bool isChanged = false;

    void Start() {
        textComp = GetComponent<Text>();
    }

	// Update is called once per frame
	void Update () {
        prevScore = GameManager.instance.Score;
        textComp.text = GetThousandCommaText(prevScore);

        //if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("ScoreTextReel")) {
        //    if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f && isChanged == false) {
        //        textComp.text = GetThousandCommaText(prevScore);
        //        isChanged = true;
        //    }
        //} else {
        //    isChanged = false;
        //}

        //textComp.text = GetThousandCommaText(GameManager.instance.Score);
	}

    private string GetThousandCommaText(int data) {
        return string.Format("{0:#,##0}", data);
    }
}
