using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudBehaviour : MonoBehaviour {

    public Image blackScreen;

    public Text gameName;
    public Text studioName;
    public Text members;
    public Button exit;
	// Use this for initialization
	void Start () {
        StartCoroutine(FadeIn(1.0f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FadeIn()
    {
        StartCoroutine(FadeIn(1.0f));
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

    public void FadeOut()
    {
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        Color initialColor = blackScreen.GetComponent<Image>().color;
        Color c = initialColor;
        blackScreen.gameObject.SetActive(true);
        Color finalColor = new Color(0, 0, 0, 1);
        float elapsedTime = 0.0f;
        while (elapsedTime < 2.0f)
        {
            elapsedTime += Time.deltaTime;
            c = Color.Lerp(initialColor, finalColor, elapsedTime / 2.0f);
            blackScreen.GetComponent<Image>().color = c;
            yield return null;
        }
        blackScreen.GetComponent<Image>().color = finalColor;
    }

    public void showFinalInfo()
    {
        StartCoroutine(showInfo());
    }

    IEnumerator showInfo()
    {
        yield return new WaitForSeconds(3);
        Color initialColor = gameName.color;
        Color c = initialColor;
        Color finalColor = new Color(255, 255, 255, 1);
        float elapsedTime = 0.0f;
        while (elapsedTime < 1.0f)
        {
            elapsedTime += Time.deltaTime;
            c = Color.Lerp(initialColor, finalColor, elapsedTime / 1.0f);
            gameName.color = c;
            yield return null;
        }
        gameName.color = finalColor;
        yield return new WaitForSeconds(1);
        elapsedTime = 0.0f;
        while (elapsedTime < 1.0f)
        {
            elapsedTime += Time.deltaTime;
            c = Color.Lerp(initialColor, finalColor, elapsedTime / 1.0f);
            studioName.color = c;
            yield return null;
        }
        studioName.color = finalColor;
        yield return new WaitForSeconds(1);
        elapsedTime = 0.0f;
        while (elapsedTime < 1.0f)
        {
            elapsedTime += Time.deltaTime;
            c = Color.Lerp(initialColor, finalColor, elapsedTime / 1.0f);
            members.color = c;
            yield return null;
        }
        members.color = finalColor;
        yield return new WaitForSeconds(2);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        exit.gameObject.SetActive(true);
    }
}
