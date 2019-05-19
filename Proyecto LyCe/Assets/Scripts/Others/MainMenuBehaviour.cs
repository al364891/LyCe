using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour {

    public Button newGame, loadGame, options, exit, volumen, controles, atrasOpciones, atrasVolumen, atrasControles;
    public Slider volumenControl;

    public Image blackScreen;

    public void newGameFunc()
    {
        Debug.Log("newGame");
        PlayerPrefs.SetInt("banyo2Planta2Key", 0);
        PlayerPrefs.SetInt("childBoxKey", 0);
        PlayerPrefs.SetInt("ganchoKey", 0);
        PlayerPrefs.SetInt("tenazasKey", 0);
        PlayerPrefs.SetInt("ganzuaKey", 0);
        PlayerPrefs.SetInt("axeKey", 0);
        PlayerPrefs.SetInt("boxSalonKey", 0);
        PlayerPrefs.SetInt("boxBuhardillaKey", 0);
        PlayerPrefs.SetInt("palancaKey", 0);
        PlayerPrefs.SetInt("lantern", 0);
        PlayerPrefs.SetInt("salonBoxKey", 0);
        PlayerPrefs.SetInt("acidoKey", 0);
        PlayerPrefs.SetInt("pasilloChildKey", 0);
        StartCoroutine(FadeOut(1.0f));
    }

    public void loadGameFunc()
    {
        Debug.Log("loadGame");
        StartCoroutine(FadeOut(1.0f));
    }

    public void optionsFunc()
    {
        newGame.gameObject.SetActive(false);
        loadGame.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
        volumen.gameObject.SetActive(true);
        controles.gameObject.SetActive(true);
        atrasOpciones.gameObject.SetActive(true);
    }

    public void exitFunc()
    {
        Application.Quit();
    }

    public void volumenFunc()
    {
        atrasOpciones.gameObject.SetActive(false);
        controles.gameObject.SetActive(false);
        volumen.gameObject.SetActive(false);
        volumenControl.gameObject.SetActive(true);
        atrasVolumen.gameObject.SetActive(true);
    }

    public void cambiarVolumen()
    {
        //AudioManager.Instance.ChangeGeneralVolume(volumenControl.value);
    }

    public void controlesFunc()
    {
        controles.gameObject.SetActive(false);
        volumen.gameObject.SetActive(false);
        atrasOpciones.gameObject.SetActive(false);
        atrasControles.gameObject.SetActive(true);
    }

    public void atrasOpcionesFunc()
    {
        newGame.gameObject.SetActive(true);
        loadGame.gameObject.SetActive(true);
        options.gameObject.SetActive(true);
        exit.gameObject.SetActive(true);
        volumen.gameObject.SetActive(false);
        controles.gameObject.SetActive(false);
        atrasOpciones.gameObject.SetActive(false);
    }

    public void atrasVolumenFunc()
    {
        atrasOpciones.gameObject.SetActive(true);
        controles.gameObject.SetActive(true);
        volumen.gameObject.SetActive(true);
        volumenControl.gameObject.SetActive(false);
        atrasVolumen.gameObject.SetActive(false);
    }

    public void atrasControlesFunc()
    {
        controles.gameObject.SetActive(true);
        volumen.gameObject.SetActive(true);
        atrasOpciones.gameObject.SetActive(true);
        atrasControles.gameObject.SetActive(false);
    }

    IEnumerator FadeOut(float time)
    {
        Color initialColor = blackScreen.GetComponent<Image>().color;
        Color c = initialColor;
        blackScreen.gameObject.SetActive(true);
        Color finalColor = new Color(0, 0, 0, 1);
        float elapsedTime = 0.0f;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            c = Color.Lerp(initialColor, finalColor, elapsedTime / time);
            blackScreen.GetComponent<Image>().color = c;
            yield return null;
        }
        blackScreen.GetComponent<Image>().color = finalColor;
        SceneManager.LoadScene(1);
    }
}
