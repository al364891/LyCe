using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour {

    //creacion de la camara
    public Camera camera;

    //variables publicas que harán las MODIFICACIONES en los giros verticales y horizontales
    public float horizontalSpeed;
    public float verticalSpeed;

    //variables FINALES donde asignaremos las velocidades
    float h;
    float v;

    void Start()
    {
        //nada que ver con el seguimiento de la cámara
        //con esto solo hago que no exista el ratón en la escena
        Cursor.visible = false;
    }

        void Update ()
    {
        //h y v: velocidad de movimiento * eje X o Y
        //para tener el resultado de "cuanto se ha movido" y "en que dirección"
        h = horizontalSpeed * Input.GetAxis("Mouse X");
        v = verticalSpeed * Input.GetAxis("Mouse Y");

        //aplicamos la rotación HORIZONTAL del PJ
        transform.Rotate(0,h,0);
        //aplicamos la rotación VERTICAL de la cámara
        camera.transform.Rotate(-v, 0, 0);
    }
}
