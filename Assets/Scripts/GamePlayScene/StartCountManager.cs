using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCountManager : MonoBehaviour {

    public Text txtCount;

    private float timeSpan = 0f;
	
    void Start() {
        timeSpan = 0f;
    }

	// Update is called once per frame
	void Update () {
        timeSpan += Time.deltaTime;

        if (timeSpan >= 3.0f) {
            gameObject.SetActive(false);
            GameManager.instance.GameStart();
            Destroy(gameObject);
        }
        else if (timeSpan >= 2.0f)
            txtCount.text = "1";
        else if (timeSpan >= 1.0f)
            txtCount.text = "2";
        else
            txtCount.text = "3";
	}
}
