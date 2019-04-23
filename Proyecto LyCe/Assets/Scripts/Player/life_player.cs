using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class life_player : MonoBehaviour {
    public int life;

    public HudBehaviour hud;

    Vector3 initial_position;

    private int initial_life;

    // Use this for initialization
    void Start()
    {
        initial_position = this.transform.position;
        initial_life = life;
    }
	
	// Update is called once per frame
	void Update () {
		if (life == 0)
        {
            hud.FadeOut();
            StartCoroutine(Restart());
        }
	}

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(3);
        this.transform.position = initial_position;
        hud.FadeIn();
        life = initial_life;
    }
}
