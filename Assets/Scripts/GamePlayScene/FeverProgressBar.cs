using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeverProgressBar : MonoBehaviour {

    public GameObject imgTip;

    private Image image;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        image.fillAmount = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.IsFeverMode)
            return;

        GetComponent<Animator>().SetBool("bFeverMode", false);

        image.fillAmount += (GameManager.instance.FeverProgress - image.fillAmount) * Time.deltaTime * 2f;
        if(image.fillAmount >= 0.1f) {
            imgTip.SetActive(true);
        } else {
            imgTip.SetActive(false);
        }
        imgTip.GetComponent<RectTransform>().anchorMin = new Vector2(image.fillAmount - 0.1f, imgTip.GetComponent<RectTransform>().anchorMin.y);
        imgTip.GetComponent<RectTransform>().anchorMax = new Vector2(image.fillAmount - 0.1f, imgTip.GetComponent<RectTransform>().anchorMax.y);
        imgTip.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        if(image.fillAmount >= 1.0f && !GameManager.instance.IsFeverMode) {
            GameManager.instance.FeverMode();
            imgTip.SetActive(false);
            GetComponent<Animator>().SetBool("bFeverMode", true);
        }
	}
}
