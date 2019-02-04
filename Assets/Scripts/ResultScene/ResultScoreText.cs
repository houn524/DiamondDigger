using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScoreText : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = GetThousandCommaText(GameManager.instance.Score);
	}

    private string GetThousandCommaText(int data) {
        return string.Format("{0:#,##0}", data);
    }
}
