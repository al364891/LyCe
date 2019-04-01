using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxes : MonoBehaviour {

    public enum Box { childBox, parentsBox, hallBox, buhardillaBox }
    private bool opened = false;
    public Box whatBoxAmI;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void controlBox()
    {
        if (!opened)
        {
            opened = true;
            anim.SetBool("Abierto", opened);
        }
    }
}
