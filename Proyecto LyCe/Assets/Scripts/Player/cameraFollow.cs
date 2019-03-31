using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour {

    //creacion de la camara
    public Camera camera;

    //variables publicas que harán las MODIFICACIONES en los giros verticales y horizontales
    public float horizontalSpeed=2.0f;
    public float verticalSpeed=2.0f;

    //variables FINALES donde asignaremos las velocidades
    float h;
    float v;
    float maxV;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public float distanceToSee;
    RaycastHit whatIHit;
    PlayerMovement playerMovement;
    public Inventory inventory;

    void Start()
    {
        maxV = 0;

        //nada que ver con el seguimiento de la cámara
        //con esto solo hago que no exista el ratón en la escena
        Cursor.visible = false;
        //con esto el raton "siempre estará centrado en el centro de la pantalla"
        Cursor.lockState = CursorLockMode.Locked;
        playerMovement = GetComponent<PlayerMovement>();
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
        yaw -= horizontalSpeed * Input.GetAxis("Mouse Y");
        pitch += verticalSpeed * Input.GetAxis("Mouse X");

        if (yaw >= 90.0f)
        {
            yaw = 90.0f;
        }else if (yaw <= -90.0f)
        {
            yaw = -90.0f;
        }

        transform.eulerAngles = new Vector3 (0, pitch, 0);

        camera.transform.eulerAngles = new Vector3(yaw, pitch, 0);

    }

        private void rayController()
    {
        Debug.DrawRay(camera.transform.position, camera.transform.forward * distanceToSee, Color.magenta);

        if (Physics.Raycast(camera.transform.position, this.transform.forward, out whatIHit, distanceToSee))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("I touched " + whatIHit.collider.gameObject.name);
                if (whatIHit.collider.tag == "Puerta")
                {
                    if (whatIHit.collider.gameObject.GetComponent<Doors>().whatDoorAmI == Doors.Door.childDoor)
                    {
                        // if () //En este if ira si el jugador tiene el ítem necesario para abrirlo, por ahora se abre siempre
                        whatIHit.collider.gameObject.GetComponent<Doors>().controlDoor();
                    } else if (whatIHit.collider.gameObject.GetComponent<Doors>().whatDoorAmI == Doors.Door.pasilloChildDoor)
                    {
                        if (inventory.hasChildBoxKey)
                            whatIHit.collider.gameObject.GetComponent<Doors>().controlDoor();
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<Doors>().whatDoorAmI == Doors.Door.banyo2Planta2)
                    {
                        if (inventory.hasBanyo2Planta2Key)
                            whatIHit.collider.gameObject.GetComponent<Doors>().controlDoor();
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<Doors>().whatDoorAmI == Doors.Door.sisterDoor)
                    {
                        whatIHit.collider.gameObject.GetComponent<Doors>().controlDoor();
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<Doors>().whatDoorAmI == Doors.Door.parentsDoor)
                    {
                        if (inventory.hasTenazasKey)
                            whatIHit.collider.gameObject.GetComponent<Doors>().controlDoor();
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<Doors>().whatDoorAmI == Doors.Door.banyo1Planta2)
                    {
                        whatIHit.collider.gameObject.GetComponent<Doors>().controlDoor();
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<Doors>().whatDoorAmI == Doors.Door.recibidorDerecha)
                    {
                        if (inventory.hasAxeKey)
                            whatIHit.collider.gameObject.GetComponent<Doors>().controlDoor();
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<Doors>().whatDoorAmI == Doors.Door.recibidorIzquierda)
                    {
                        if (inventory.hasGanzuaKey)
                            whatIHit.collider.gameObject.GetComponent<Doors>().controlDoor();
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<Doors>().whatDoorAmI == Doors.Door.puertaSalon)
                    {
                        whatIHit.collider.gameObject.GetComponent<Doors>().controlDoor();
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<Doors>().whatDoorAmI == Doors.Door.puertaCocina)
                    {
                        whatIHit.collider.gameObject.GetComponent<Doors>().controlDoor();
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<Doors>().whatDoorAmI == Doors.Door.banyoPlanta1)
                    {
                        whatIHit.collider.gameObject.GetComponent<Doors>().controlDoor();
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<Doors>().whatDoorAmI == Doors.Door.puertaGaraje1)
                    {
                        whatIHit.collider.gameObject.GetComponent<Doors>().controlDoor();
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<Doors>().whatDoorAmI == Doors.Door.puertaGaraje2)
                    {
                        whatIHit.collider.gameObject.GetComponent<Doors>().controlDoor();
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<Doors>().whatDoorAmI == Doors.Door.ladder)
                    {
                        if (inventory.hasGanchoKey)
                            whatIHit.collider.gameObject.GetComponent<Doors>().controlLadder();
                    }
                } else if (whatIHit.collider.tag == "ElementoClave")
                {
                    if (whatIHit.collider.gameObject.GetComponent<KeyObjects>().whatKeyIPick == KeyObjects.KeyObject.banyo2Planta2Key)
                    {
                        Destroy(whatIHit.collider.gameObject);
                        inventory.hasBanyo2Planta2Key = true;
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<KeyObjects>().whatKeyIPick == KeyObjects.KeyObject.childBoxKey)
                    {
                        Destroy(whatIHit.collider.gameObject);
                        inventory.hasChildBoxKey = true;
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<KeyObjects>().whatKeyIPick == KeyObjects.KeyObject.ganchoKey)
                    {
                        Destroy(whatIHit.collider.gameObject);
                        inventory.hasGanchoKey = true;
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<KeyObjects>().whatKeyIPick == KeyObjects.KeyObject.tenazasKey)
                    {
                        Destroy(whatIHit.collider.gameObject);
                        inventory.hasTenazasKey = true;
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<KeyObjects>().whatKeyIPick == KeyObjects.KeyObject.ganzuaKey)
                    {
                        Destroy(whatIHit.collider.gameObject);
                        inventory.hasGanzuaKey = true;
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<KeyObjects>().whatKeyIPick == KeyObjects.KeyObject.axeKey)
                    {
                        Destroy(whatIHit.collider.gameObject);
                        inventory.hasAxeKey = true;
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<KeyObjects>().whatKeyIPick == KeyObjects.KeyObject.boxSalonKey)
                    {
                        Destroy(whatIHit.collider.gameObject);
                        inventory.hasBoxSalonKey = true;
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<KeyObjects>().whatKeyIPick == KeyObjects.KeyObject.boxBuhardillaKey)
                    {
                        Destroy(whatIHit.collider.gameObject);
                        inventory.hasBoxBuhardillaKey = true;
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<KeyObjects>().whatKeyIPick == KeyObjects.KeyObject.palancaKey)
                    {
                        Destroy(whatIHit.collider.gameObject);
                        inventory.hasPalancaKey = true;
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<KeyObjects>().whatKeyIPick == KeyObjects.KeyObject.lantern)
                    {
                        Destroy(whatIHit.collider.gameObject);
                        playerMovement.lanternTaked = true;
                    }
                } else if (whatIHit.collider.tag == "Caja")
                {
                    if (whatIHit.collider.gameObject.GetComponent<Boxes>().whatBoxAmI == Boxes.Box.childBox)
                    {
                        whatIHit.collider.gameObject.GetComponent<Boxes>().controlBox();
                    } else if (whatIHit.collider.gameObject.GetComponent<Boxes>().whatBoxAmI == Boxes.Box.parentsBox)
                    {
                        whatIHit.collider.gameObject.GetComponent<Boxes>().controlBox();
                    } else if (whatIHit.collider.gameObject.GetComponent<Boxes>().whatBoxAmI == Boxes.Box.hallBox)
                    {
                        whatIHit.collider.gameObject.GetComponent<Boxes>().controlBox();
                    }
                }
            }
        }
    }
}
