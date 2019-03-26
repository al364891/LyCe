using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour {

    public enum Door {childDoor, pasilloChildDoor, banyo2Planta2, sisterDoor, parentsDoor, banyo1Planta2, recibidorDerecha, recibidorIzquierda, puertaSalon, puertaCocina, banyoPlanta1, puertaGaraje1, puertaGaraje2, ladder }
    private bool opened = false;
    public Door whatDoorAmI;

    public Animator anim;

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
    }

    public void controlLadder()
    {
        opened = true;
        anim.SetBool("Bajada", opened);
    }
}
