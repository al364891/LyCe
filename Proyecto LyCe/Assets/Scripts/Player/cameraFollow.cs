using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class cameraFollow : MonoBehaviour {

    public AudioSource audioDoorOpen;
    public AudioSource audioDoorClose;
    public AudioSource audioKey;
    public AudioSource audioBox;
    //AudioSource audioSource;

    public AudioSource audioUnlocked;

    bool controlCloseDoor = false;
    bool controlOpenDoor = false;

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

    public GameObject candado;
    public InformationText infoText;

    public HudBehaviour hud;

    public PauseMenuBehaviour pauseMenu;

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
                        controlOpenDoor = true;
                    } else if (whatIHit.collider.gameObject.GetComponent<Doors>().whatDoorAmI == Doors.Door.pasilloChildDoor)
                    {
                        controlDoor(inventory.hasPasilloChildKey);
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<Doors>().whatDoorAmI == Doors.Door.banyo2Planta2)
                    {
                        controlDoor(inventory.hasBanyo2Planta2Key);
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<Doors>().whatDoorAmI == Doors.Door.sisterDoor)
                    {
                        whatIHit.collider.gameObject.GetComponent<Doors>().controlDoor();
                        controlOpenDoor = true;
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<Doors>().whatDoorAmI == Doors.Door.parentsDoor)
                    {
                        controlDoor(inventory.hasTenazasKey);
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<Doors>().whatDoorAmI == Doors.Door.banyo1Planta2)
                    {
                        whatIHit.collider.gameObject.GetComponent<Doors>().controlDoor();
                        controlOpenDoor = true;
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<Doors>().whatDoorAmI == Doors.Door.recibidorDerecha)
                    {
                        controlDoor(inventory.hasAxeKey);
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<Doors>().whatDoorAmI == Doors.Door.recibidorIzquierda)
                    {
                        controlDoor(inventory.hasGanzuaKey);
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<Doors>().whatDoorAmI == Doors.Door.puertaSalon)
                    {
                        whatIHit.collider.gameObject.GetComponent<Doors>().controlDoor();
                        controlOpenDoor = true;
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<Doors>().whatDoorAmI == Doors.Door.puertaCocina)
                    {
                        whatIHit.collider.gameObject.GetComponent<Doors>().controlDoor();
                        controlOpenDoor = true;
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<Doors>().whatDoorAmI == Doors.Door.banyoPlanta1)
                    {
                        whatIHit.collider.gameObject.GetComponent<Doors>().controlDoor();
                        controlOpenDoor = true;
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<Doors>().whatDoorAmI == Doors.Door.puertaGaraje1)
                    {
                        controlDoor(inventory.hasSalonBoxKey);
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<Doors>().whatDoorAmI == Doors.Door.puertaGaraje2)
                    {
                        /*if (inventory.hasAcidoKey)
                        {
                            whatIHit.collider.gameObject.GetComponent<Doors>().controlDoor();
                            if (candado != null)
                                Destroy(candado);
                        }*/
                        controlDoor(inventory.hasAcidoKey);
                        if (candado != null)
                        {
                            Destroy(candado);
                            audioUnlocked.Play();
                        }
                            
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<Doors>().whatDoorAmI == Doors.Door.ladder)
                    {
                        if (inventory.hasGanchoKey)
                            whatIHit.collider.gameObject.GetComponent<Doors>().controlLadder();
                    }

                }
                else if (whatIHit.collider.tag == "ElementoClave")
                {
                    if (whatIHit.collider.gameObject.GetComponent<KeyObjects>().whatKeyIPick == KeyObjects.KeyObject.banyo2Planta2Key)
                    {
                        /*Destroy(whatIHit.collider.gameObject);
                        inventory.hasBanyo2Planta2Key = true;
                        
                        infoText.text.text = "You have taken the key for the bathroom!";
                        infoText.showText();*/
                        PlayerPrefs.SetInt("banyo2Planta2Key", 1);
                        inventory.hasBanyo2Planta2Key = true;
                        controlKey("Mmmm, a key. What can it open?");
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<KeyObjects>().whatKeyIPick == KeyObjects.KeyObject.childBoxKey)
                    {
                        /*Destroy(whatIHit.collider.gameObject);
                        inventory.hasChildBoxKey = true;
                        
                        infoText.text.text = "You have taken the key for the box of your room!";
                        infoText.showText();*/
                        PlayerPrefs.SetInt("childBoxKey", 1);
                        inventory.hasChildBoxKey = true;
                        controlKey("This key should be for my room box.");
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<KeyObjects>().whatKeyIPick == KeyObjects.KeyObject.ganchoKey)
                    {
                        /*Destroy(whatIHit.collider.gameObject);
                        inventory.hasGanchoKey = true;
                        
                        infoText.text.text = "You have taken a hook!";
                        infoText.showText();*/
                        PlayerPrefs.SetInt("ganchoKey", 1);
                        inventory.hasGanchoKey = true;
                        controlKey("I can put down the stairs of the attic.");
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<KeyObjects>().whatKeyIPick == KeyObjects.KeyObject.tenazasKey)
                    {
                        /*Destroy(whatIHit.collider.gameObject);
                        inventory.hasTenazasKey = true;
                        
                        infoText.text.text = "You have taken pincers!";
                        infoText.showText();*/
                        PlayerPrefs.SetInt("tenazasKey", 1);
                        inventory.hasTenazasKey = true;
                        controlKey("Maybe I can force some door with the pincers.");
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<KeyObjects>().whatKeyIPick == KeyObjects.KeyObject.ganzuaKey)
                    {
                        /*Destroy(whatIHit.collider.gameObject);
                        inventory.hasGanzuaKey = true;
                        
                        infoText.text.text = "You have taken a lock pick!";
                        infoText.showText();*/
                        PlayerPrefs.SetInt("ganzuaKey", 1);
                        inventory.hasGanzuaKey = true;
                        controlKey("This lock pick would be very useful.");
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<KeyObjects>().whatKeyIPick == KeyObjects.KeyObject.axeKey)
                    {
                        /*Destroy(whatIHit.collider.gameObject);
                        inventory.hasAxeKey = true;
                        
                        infoText.text.text = "You have taken a axe!";
                        infoText.showText();*/
                        PlayerPrefs.SetInt("axeKey", 1);
                        inventory.hasAxeKey = true;
                        controlKey("I will bring down the kitchen door.");
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<KeyObjects>().whatKeyIPick == KeyObjects.KeyObject.boxSalonKey)
                    {
                        /*Destroy(whatIHit.collider.gameObject);
                        inventory.hasBoxSalonKey = true;
                        
                        infoText.text.text = "You have taken a key for the box of the living room!";
                        infoText.showText();*/
                        PlayerPrefs.SetInt("boxSalonKey", 1);
                        inventory.hasBoxSalonKey = true;
                        controlKey("Wasn't it a box on the living room?");
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<KeyObjects>().whatKeyIPick == KeyObjects.KeyObject.boxBuhardillaKey)
                    {
                        /*Destroy(whatIHit.collider.gameObject);
                        inventory.hasBoxBuhardillaKey = true;
                        
                        infoText.text.text = "You have taken a key for the attic!";
                        infoText.showText();*/
                        PlayerPrefs.SetInt("boxBuhardillaKey", 1);
                        inventory.hasBoxBuhardillaKey = true;
                        controlKey("Another box key, this should be for the attic one.");
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<KeyObjects>().whatKeyIPick == KeyObjects.KeyObject.palancaKey)
                    {
                        /*Destroy(whatIHit.collider.gameObject);
                        inventory.hasPalancaKey = true;
                        
                        infoText.text.text = "You have taken a lever!";
                        infoText.showText();*/
                        PlayerPrefs.SetInt("palancaKey", 1);
                        inventory.hasPalancaKey = true;
                        controlKey("Time to take down some door.");
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<KeyObjects>().whatKeyIPick == KeyObjects.KeyObject.lantern)
                    {
                        /*Destroy(whatIHit.collider.gameObject);
                        playerMovement.lanternTaked = true;
                        
                        infoText.text.text = "You have taken the lantern!";
                        infoText.showText();
                        /*----------------------------------------------*/
                        PlayerPrefs.SetInt("lantern", 1);
                        playerMovement.lanternTaked = true;
                        controlKey("This lantern will be very useful.");
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<KeyObjects>().whatKeyIPick == KeyObjects.KeyObject.carKeys)
                    {
                        /*Destroy(whatIHit.collider.gameObject);
                        inventory.hasCarKeys = true;
                        //     Este objeto, al ser el último igual cunde no meterle autoguardado
                        infoText.text.text = "You have taken the car keys!";
                        infoText.showText();
                        /*-----------------------------------------------*/
                        PlayerPrefs.SetInt("carKeys", 1);
                        inventory.hasCarKeys = true;
                        controlKey("Oh god, the car keys, this is my ticket to leave this house!");
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<KeyObjects>().whatKeyIPick == KeyObjects.KeyObject.salonBoxKey && inventory.salonBoxOpen)
                    {
                        /*Destroy(whatIHit.collider.gameObject);
                        inventory.hasSalonBoxKey = true;
                        
                        infoText.text.text = "You have taken a key for the living room!";
                        infoText.showText();*/
                        PlayerPrefs.SetInt("salonBoxKey", 1);
                        inventory.hasSalonBoxKey = true;
                        controlKey("This key should be for the living room.");
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<KeyObjects>().whatKeyIPick == KeyObjects.KeyObject.acidoKey && inventory.buhardillaBoxOpen)
                    {
                        /*Destroy(whatIHit.collider.gameObject);
                        inventory.hasAcidoKey = true;
                        
                        infoText.text.text = "You have taken an acid canister!";
                        infoText.showText();*/
                        PlayerPrefs.SetInt("acidoKey", 1);
                        inventory.hasAcidoKey = true;
                        controlKey("I could use this to open the padlock that I have seen before.");
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<KeyObjects>().whatKeyIPick == KeyObjects.KeyObject.pasilloChildKey && inventory.childBoxOpen)
                    {
                        /*Destroy(whatIHit.collider.gameObject);
                        inventory.hasPasilloChildKey = true;
                        
                        infoText.text.text = "You have taken a key for the corridor!";
                        infoText.showText();*/
                        PlayerPrefs.SetInt("pasilloChildKey", 1);
                        inventory.hasPasilloChildKey = true;
                        controlKey("It's time to explore where I am.");
                    }
                } else if (whatIHit.collider.tag == "Caja")
                {
                    if (whatIHit.collider.gameObject.GetComponent<Boxes>().whatBoxAmI == Boxes.Box.childBox)
                    {

                        //audioBox.clip = box;
                        audioBox.Play();

                        if (inventory.hasChildBoxKey)
                        {
                            whatIHit.collider.gameObject.GetComponent<Boxes>().controlBox();
                            whatIHit.collider.enabled = false;
                            inventory.childBoxOpen = true;
                        }
                    } else if (whatIHit.collider.gameObject.GetComponent<Boxes>().whatBoxAmI == Boxes.Box.parentsBox)
                    {
                        if (inventory.hasPalancaKey)
                        {
                            whatIHit.collider.gameObject.GetComponent<Boxes>().controlBox();
                            whatIHit.collider.enabled = false;
                            inventory.parentsBoxOpen = true;
                        }
                    } else if (whatIHit.collider.gameObject.GetComponent<Boxes>().whatBoxAmI == Boxes.Box.hallBox)
                    {
                        if (inventory.hasBoxSalonKey)
                        {
                            whatIHit.collider.gameObject.GetComponent<Boxes>().controlBox();
                            whatIHit.collider.enabled = false;
                            inventory.salonBoxOpen = true;
                        }
                    }
                    else if (whatIHit.collider.gameObject.GetComponent<Boxes>().whatBoxAmI == Boxes.Box.buhardillaBox)
                    {
                        if (inventory.hasBoxBuhardillaKey)
                        {
                            whatIHit.collider.gameObject.GetComponent<Boxes>().controlBox();
                            whatIHit.collider.enabled = false;
                            inventory.buhardillaBoxOpen = true;
                        }
                    }
                } else if (whatIHit.collider.tag== "FinJuego")
                {
                    if (inventory.hasCarKeys)
                    {
                        pauseMenu.enabled = false;
                        playerMovement.enabled = false;
                        hud.FadeOut();
                        hud.showFinalInfo();
                    }
                }
            }

            if (controlCloseDoor)
            {
                //audioSource.clip = doorClose;
                audioDoorClose.Play();
                controlCloseDoor = false;
            }
            else if (controlOpenDoor)
            {
                //audioSource.clip = doorOpen;
                audioDoorOpen.Play();
                controlOpenDoor = false;
            }

        }
    }

    public void controlDoor(bool hasObject)
    {
        if (hasObject)
        {
            whatIHit.collider.gameObject.GetComponent<Doors>().controlDoor();
            controlOpenDoor = true;
        }
        else
        {
            controlCloseDoor = true;
        }
    }
    
    public void controlKey(string text)
    {
        Destroy(whatIHit.collider.gameObject);
        infoText.text.text = text;
        infoText.showText();

        //audioSource.clip = key;
        audioKey.Play();
    }
    
}
