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
    float maxV;

    public float distanceToSee;
    RaycastHit whatIHit;

    void Start()
    {
        maxV = 0;

        //nada que ver con el seguimiento de la cámara
        //con esto solo hago que no exista el ratón en la escena
        Cursor.visible = false;
        //con esto el raton "siempre estará centrado en el centro de la pantalla"
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update ()
    {
        cameraMovement();

        rayController();
    }

    private void cameraMovement()
    {
        //h y v: velocidad de movimiento * eje X o Y
        //para tener el resultado de "cuanto se ha movido" y "en que dirección"
        h = horizontalSpeed * Input.GetAxis("Mouse X");
        v = verticalSpeed * Input.GetAxis("Mouse Y");
        maxV += v;

        //miramos que la camara tenga un tope para mirar "hacia arriba y hacia abajo"
        if (maxV > 90.0f)
        {
            maxV = 90;
            v = 0.0f;
        }
        else if (maxV < -90.0f)
        {
            maxV = -90;
            v = 0.0f;
        }

        //aplicamos la rotación HORIZONTAL del PJ
        transform.Rotate(0, h, 0);
        //aplicamos la rotación VERTICAL de la cámara
        camera.transform.Rotate(-v, 0, 0);
    }

        private void rayController()
    {
        Debug.DrawRay(camera.transform.position, camera.transform.forward * distanceToSee, Color.magenta);

        if (Physics.Raycast(camera.transform.position, this.transform.forward, out whatIHit, distanceToSee))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Debug.Log("I touched " + whatIHit.collider.gameObject.name);
                if (whatIHit.collider.tag == "Puerta")
                {
                    if (whatIHit.collider.gameObject.GetComponent<Doors>().whatDoorAmI == Doors.Door.childDoor)
                    {
                        // if () //En este if ira si el jugador tiene el ítem necesario para abrirlo, por ahora se abre siempre
                        whatIHit.collider.gameObject.GetComponent<Doors>().controlDoor();
                        Debug.Log("Hola");
                    }
                }
            }
        }
    }
}
