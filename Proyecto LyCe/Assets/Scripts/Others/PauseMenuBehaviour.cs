using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PauseMenuBehaviour : MonoBehaviour
{

    public Button resume, options, exit, volumen, controles, atrasOpciones, atrasVolumen, atrasControles;
    public Slider volumenControl;

    public Image blackScreen;
    private bool pause = false;

    public PlayerMovement playerMovement;
    public cameraFollow cameraFollow;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = pauseFunc();
        }
    }

    private bool pauseFunc()
    {
        if (pause == true)
        {
            if(volumen.IsActive())
            {
                atrasOpcionesFunc();
            }
            else if (volumenControl.IsActive())
            {
                atrasVolumenFunc();
            }
            else if (atrasControles.IsActive())
            {
                atrasControlesFunc();
            }
            else
            {
                blackScreen.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                resume.gameObject.SetActive(false);
                options.gameObject.SetActive(false);
                exit.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
                playerMovement.enabled = true;
                cameraFollow.enabled = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                return false;
            }
            return true;
        }
        else
        {
            blackScreen.GetComponent<Image>().color = new Color(0, 0, 0, 1);
            resume.gameObject.SetActive(true);
            options.gameObject.SetActive(true);
            exit.gameObject.SetActive(true);
            Time.timeScale = 0.0f;
            playerMovement.enabled = false;
            cameraFollow.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            return true;
        }
    }

    public void resumeFunc()
    {
        blackScreen.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        resume.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
        playerMovement.enabled = false;
        Time.timeScale = 1.0f;
        playerMovement.enabled = true;
        cameraFollow.enabled = true;
        pause = false;
    }

    public void optionsFunc()
    {
        resume.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
        volumen.gameObject.SetActive(true);
        controles.gameObject.SetActive(true);
        atrasOpciones.gameObject.SetActive(true);
    }

    public void exitFunc()
    {
        blackScreen.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        resume.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
        playerMovement.enabled = false;
        Time.timeScale = 1.0f;
        playerMovement.enabled = true;
        cameraFollow.enabled = true;
        pause = false;
        SceneManager.LoadScene(0);
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
        resume.gameObject.SetActive(true);
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
}

