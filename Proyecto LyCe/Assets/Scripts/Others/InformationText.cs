using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationText : MonoBehaviour {

    public TextMeshProUGUI text;

    private bool showing = false;
    private float maxTime = 2.0f;
    private float actualTime = 0.0f;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (showing)
            actualTime += Time.deltaTime;

		if (actualTime >= maxTime)
        {
            anim.SetBool("Show", false);
            showing = false;
            actualTime = 0.0f;
        }
	}

    public void showText()
    {
        anim.SetBool("Show", true);
        showing = true;
    }
}
