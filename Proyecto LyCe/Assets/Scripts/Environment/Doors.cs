using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour {

    public enum Door {childDoor, pasilloChildDoor, banyo2Planta2, sisterDoor, parentsDoor, banyo1Planta2, recibidorDerecha, recibidorIzquierda, puertaSalon, puertaCocina, banyoPlanta1, puertaGaraje1, puertaGaraje2, ladder }
    private bool opened = false, moving = false;
    public Door whatDoorAmI;
    private float count = 0.0f;

    public Animator anim;
    private Collider coll;

    void Start()
    {
        coll = GetComponent<Collider>();
    }

    void FixedUpdate()
    {
        if (moving)
        {
            count += Time.deltaTime;
            if (count > 1.0f)
            {
                moving = false;
                coll.enabled = true;
            }
        } else
        {
            count = 0.0f;
        }
    }

    public void controlDoor()
    {
        if (!opened)
        {
            opened = true;
            anim.SetBool("Abierto", opened);
        } else
        {
            opened = false;
            anim.SetBool("Abierto", opened);
        }
        moving = true;
        coll.enabled = false;
    }

    public void controlLadder()
    {
        opened = true;
        anim.SetBool("Bajada", opened);
    }
}
