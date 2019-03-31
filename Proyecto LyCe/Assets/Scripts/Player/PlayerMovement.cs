using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    public bool isCrouching; //Indica si esta agachado

    public GameObject eyes;

    private float speed; //Variable donde se guarda la velocidad de cada momento
    private float w_speed = 8f; //Walking speed
    private float l_speed = 7f; //Lateral speed
    private float r_speed = 15f; //Running speed
    private float c_speed = 5f; //Crouching speed
    public float rotSpeed; //Velocidad de rotacion
    private float gravity = 20f;
    private Vector3 moveDir;
    private bool lightOn = false;
    public bool lanternTaked = false; //Esto es para cuando no hayas obtenido aún la linterna, que lo pondremos a false al empezar la partida y cuando se coja a true

    CharacterController controller;
    Animator anim;
    public Light lantern;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        if (!lightOn) //Al empezar el juego la tendrás apagada
            lantern.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        characterControll();

        if (lanternTaked && Input.GetMouseButtonDown(1)) //Si tienes la linterna, lo que pasa cuando clickas en pantalla
            CheckLantern();
    }

    private void characterControll()
    {
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            { //Cuando pulsas el boton control izquierdo te agachas o levantas
                if (isCrouching) //Te levantas
                {
                    isCrouching = false;
                    anim.SetBool("Crouched", false);
                    controller.height = 2; //Modifica el tamano del collider para la nueva posicion
                    controller.center = new Vector3(0, 0.8f, 0); //Cambia el centro del collider

                    //posición de la camara "de pie"
                    StartCoroutine(CameraUp());
                }
                else //Te agachas
                {
                    isCrouching = true;
                    anim.SetBool("Crouched", true);
                    speed = c_speed; //Indica la velocidad que tendra agachado
                    controller.height = 1; //Modifica el tamano del collider para la nueva posicion
                    controller.center = new Vector3(0, 0.5f, 0); //Cambia el centro del collider

                    //posición de la camara "agachada"
                    StartCoroutine(CameraDown());
                }
            }

            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            moveDir = moveDir * speed;
            
            if (Input.GetKey(KeyCode.LeftShift))
            {

                //posición de la camara "corriendo"
                //eyes.transform.position = eyes.transform.parent.TransformPoint(0, 0, 0.3f);

                //Controles corriendo

                if (Input.GetKey(KeyCode.W)) //Si se mueve hacia adelante
                {
                    speed = r_speed;
                    anim.SetInteger("WalkDirection", 0);
                    anim.SetFloat("Velocity", 6f);
                }
                else if (Input.GetKey(KeyCode.S)) //Si se mueve hacia detras
                {
                    speed = w_speed;
                    anim.SetInteger("WalkDirection", 0);
                    anim.SetFloat("Velocity", -1f);
                }
                else if (Input.GetKey(KeyCode.A))
                { //Si se mueve hacia su izquierda
                    speed = l_speed;
                    anim.SetInteger("WalkDirection", 1);
                }
                else if (Input.GetKey(KeyCode.D))
                { //Si se mueve hacia su derecha
                    speed = l_speed;
                    anim.SetInteger("WalkDirection", 2);
                }
                else //Si esta quieto
                {
                    anim.SetFloat("Velocity", 0f);
                    anim.SetInteger("WalkDirection", 0);
                }
            }

            else
            {
                //posición de la camara cuando "no está corriendo"
                //eyes.transform.position = eyes.transform.parent.TransformPoint(0, 0, 0);

                if (isCrouching)
                {
                    //Controles agachado
                    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) //Si se mueve
                    {
                        anim.SetInteger("WalkDirection", 0);
                        anim.SetFloat("Velocity", 1f);
                    }
                    else //Si esta quieto
                    {
                        anim.SetInteger("WalkDirection", 0);
                        anim.SetFloat("Velocity", 0f);
                    }
                }
                else if (!isCrouching)
                {
                    speed = w_speed;
                    //Controles de pie
                    if (Input.GetKey(KeyCode.W)) //Si se mueve hacia adelante
                    {
                        anim.SetInteger("WalkDirection", 0);
                        anim.SetFloat("Velocity", 1f);
                    }
                    else if (Input.GetKey(KeyCode.S)) //Si se mueve hacia detras
                    {
                        anim.SetInteger("WalkDirection", 0);
                        anim.SetFloat("Velocity", -1f);
                    }
                    else if (Input.GetKey(KeyCode.A)) //Si se mueve hacia su izquierda
                    {
                        speed = l_speed;
                        anim.SetInteger("WalkDirection", 1);
                    }
                    else if (Input.GetKey(KeyCode.D)) //Si se mueve hacia su derecha
                    {
                        speed = l_speed;
                        anim.SetInteger("WalkDirection", 2);
                    }
                    else //Si no se mueve
                    {
                        anim.SetInteger("WalkDirection", 0);
                        anim.SetFloat("Velocity", 0f);
                    }
                }
            }
            
        }

        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }

    IEnumerator CameraDown()
    {
        float currentTime = 0.0f;
        float normalizedDelta;
        Vector3 initial = eyes.transform.position;

        while (currentTime < 0.8f)
        {
            currentTime += Time.deltaTime;
            normalizedDelta = currentTime / 0.8f;
            eyes.transform.position = Vector3.Lerp(initial, eyes.transform.parent.TransformPoint(0, -0.65f, 0.4f), normalizedDelta);
            yield return null;
        }
    }

    IEnumerator CameraUp()
    {
        float currentTime = 0.0f;
        float normalizedDelta;
        Vector3 initial = eyes.transform.position;

        while (currentTime < 1.0f)
        {
            currentTime += Time.deltaTime;
            normalizedDelta = currentTime / 1.0f;
            eyes.transform.position = Vector3.Lerp(initial, eyes.transform.parent.TransformPoint(0, 0, 0), normalizedDelta);
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player has collided with " + collision.collider.name);
    }

    private void CheckLantern() //Aoaga o enciende la linterna
    {
        if (lightOn)
        {
            lightOn = false;
            lantern.enabled = false;
        } else
        {
            lightOn = true;
            lantern.enabled = true;
        }
    }
}
