using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudBehaviour : MonoBehaviour {

    public Image blackScreen;

	// Use this for initialization
	void Start () {
        StartCoroutine(FadeIn(1.0f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator FadeIn(float time)
    {
        Color initialColor = blackScreen.GetComponent<Image>().color;
        Color c = initialColor;
        blackScreen.gameObject.SetActive(true);
        Color finalColor = new Color(0, 0, 0, 0);
        float elapsedTime = 0.0f;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            c = Color.Lerp(initialColor, finalColor, elapsedTime / time);
            blackScreen.GetComponent<Image>().color = c;
            yield return null;
        }
        blackScreen.GetComponent<Image>().color = finalColor;
    }
}
