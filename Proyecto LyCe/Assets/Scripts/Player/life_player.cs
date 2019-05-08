using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class life_player : MonoBehaviour {
    public int life;

    public HudBehaviour hud;
    public Image damageImage;

    Vector3 initial_position;

    private int initial_life;

    private float actualTime = 0f;
    [HideInInspector] public Color c, cR;

    // Use this for initialization
    void Start()
    {
        initial_position = this.transform.position;
        initial_life = life;
        c = damageImage.color;
        cR = damageImage.color;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if (life <= 0)
        {
            hud.FadeOut();
            StartCoroutine(Restart());
        } else if (life != 100)
        {
            actualTime += Time.deltaTime;
            if (actualTime >= 0.5f)
            {
                life++;
                actualTime = 0f;
                changeDamageImage();
            }
        }
	}

    public void changeDamageImage()
    {
        c.a = (100 - life) / 100f;
        damageImage.color = c;
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(3);
        this.transform.position = initial_position;
        hud.FadeIn();
        life = initial_life;
        damageImage.color = cR;
    }
}
