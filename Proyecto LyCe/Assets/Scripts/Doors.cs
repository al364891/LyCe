using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour {

    public enum Door {childDoor }
    private bool opened = false;
    public Door whatDoorAmI;

    Animator anim;
    //public Camera camera;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void controlDoor()
    {
        Debug.Log("Eyy");
        if (!opened)
        {
            opened = true;
            anim.SetBool("Abierto", opened);
        } else
        {
            opened = false;
            anim.SetBool("Abierto", opened);
        }
    }
}
