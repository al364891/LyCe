using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    public AudioClip walkPJ;
    public AudioClip RunPJ;
    public AudioSource audioWalkPJ;
    public AudioSource audioRunPJ;
    //AudioSource audioSource;

    public AudioSource lanternFail;

    public bool isCrouching; //Indica si esta agachado

    public bool isRunning;
    
    public bool walkSounds = false;
    public bool walkingSounds = false;

    public bool crouchSounds = false;
    public bool crouchingSounds = false;

    public bool runSounds = false;
    public bool runningSounds = false;
    
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
    public Light lantern;

    CharacterController controller;
    [HideInInspector] public Animator anim;

	// Use this for initialization
	void Start () {
        audioWalkPJ.clip = walkPJ;
        audioRunPJ.clip = RunPJ;

        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        if (!lightOn) //Al empezar el juego la tendrás apagada
            lantern.enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        characterControll();

        if (lanternTaked && Input.GetMouseButtonDown(1)) //Si tienes la linterna, lo que pasa cuando clickas en pantalla
            CheckLantern();

        if (lightOn)
            estropearLinterna();
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
            
            if (Input.GetKey(KeyCode.LeftShift)) //correr
            {
                isRunning = true;
                //posición de la camara "corriendo"
                //eyes.transform.position = eyes.transform.parent.TransformPoint(0, 0, 0.3f);

                //Controles corriendo

                if (Input.GetKey(KeyCode.W)) //Si se mueve hacia adelante
                {
                    speed = r_speed;
                    anim.SetInteger("WalkDirection", 0);
                    anim.SetFloat("Velocity", 6f);

                    runSounds = true;
                    isRunningSounds();
                }
                else if (Input.GetKey(KeyCode.S)) //Si se mueve hacia detras
                {
                    speed = w_speed;
                    anim.SetInteger("WalkDirection", 0);
                    anim.SetFloat("Velocity", -1f);

                    runSounds = true;
                    isRunningSounds();
                }
                else if (Input.GetKey(KeyCode.A))
                { //Si se mueve hacia su izquierda
                    speed = l_speed;
                    anim.SetInteger("WalkDirection", 1);

                    runSounds = true;
                    isRunningSounds();
                }
                else if (Input.GetKey(KeyCode.D))
                { //Si se mueve hacia su derecha
                    speed = l_speed;
                    anim.SetInteger("WalkDirection", 2);

                    runSounds = true;
                    isRunningSounds();
                }
                else //Si esta quieto
                {
                    anim.SetFloat("Velocity", 0f);
                    anim.SetInteger("WalkDirection", 0);

                    runSounds = false;
                    isRunningSounds();
                }

            }

            else
            {
                isRunning = false;
                //posición de la camara cuando "no está corriendo"
                //eyes.transform.position = eyes.transform.parent.TransformPoint(0, 0, 0);

                if (isCrouching) //esta agachado
                {
                    //Controles agachado
                    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) //Si se mueve
                    {
                        anim.SetInteger("WalkDirection", 0);
                        anim.SetFloat("Velocity", 1f);

                        crouchSounds = true;
                        isCrouchingSounds();
                    }
                    else //Si esta quieto
                    {
                        anim.SetInteger("WalkDirection", 0);
                        anim.SetFloat("Velocity", 0f);

                        crouchSounds = false;
                        isCrouchingSounds();
                    }
                }
                else if (!isCrouching) //esta andando
                {
                    speed = w_speed;
                    //Controles de pie
                    if (Input.GetKey(KeyCode.W)) //Si se mueve hacia adelante
                    {
                        anim.SetInteger("WalkDirection", 0);
                        anim.SetFloat("Velocity", 1f);

                        walkSounds = true;
                        isWalkingSounds();
                    }
                    else if (Input.GetKey(KeyCode.S)) //Si se mueve hacia detras
                    {
                        anim.SetInteger("WalkDirection", 0);
                        anim.SetFloat("Velocity", -1f);

                        walkSounds = true;
                        isWalkingSounds();
                    }
                    else if (Input.GetKey(KeyCode.A)) //Si se mueve hacia su izquierda
                    {
                        speed = l_speed;
                        anim.SetInteger("WalkDirection", 1);

                        walkSounds = true;
                        isWalkingSounds();
                    }
                    else if (Input.GetKey(KeyCode.D)) //Si se mueve hacia su derecha
                    {
                        speed = l_speed;
                        anim.SetInteger("WalkDirection", 2);

                        walkSounds = true;
                        isWalkingSounds();
                    }
                    else //Si no se mueve
                    {
                        anim.SetInteger("WalkDirection", 0);
                        anim.SetFloat("Velocity", 0f);

                        walkSounds = false;
                        isWalkingSounds();
                    }
                }
            }
            
        }

        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }
    
    /*
    public void soundsPJ()
    {
        if (isRunning && !runSounds)
        {
            if (walkSounds)
            {
                audioWalkPJ.Stop();
                walkSounds = false;

                audioRunPJ.loop = true;
                audioRunPJ.Play();
                runSounds = true;
            }
            
            
        }
        else{
            if (runSounds)
            {
                audioRunPJ.Stop();
                runSounds = false;
            }

            if (isCrouching && !walkSounds)
            {
                audioWalkPJ.volume = 0.1f;
            }
            else{
                audioWalkPJ.volume = 0.5f;
            }

            audioWalkPJ.Play();
            walkSounds = true;
        }
    }
    */

    
    public void isWalkingSounds()
    {
        if (walkSounds && !walkingSounds)
        {
            crouchingSounds = false;
            runningSounds = false;
            walkingSounds = true;
            audioWalkPJ.volume = 0.5f;
            audioRunPJ.Stop();
            audioWalkPJ.Play();
            audioWalkPJ.loop = true;
        }
        else if (!walkSounds && walkingSounds)
        {
            walkingSounds = false;
            audioWalkPJ.loop = false;
            audioWalkPJ.Stop();
        }
    }
    
    public void isCrouchingSounds()
    {

        if (crouchSounds && !crouchingSounds)
        {
            walkingSounds = false;
            runningSounds = false;
            crouchingSounds = true;
            audioWalkPJ.volume = 0.1f;
            audioRunPJ.Stop();
            audioWalkPJ.Play();
            audioWalkPJ.loop = true;
        }
        else if (!crouchSounds && crouchingSounds)
        {
            crouchingSounds = false;
            audioWalkPJ.loop = false;
            audioWalkPJ.Stop();
        }
    }
    
    public void isRunningSounds()
    {

        if (runSounds && !runningSounds)
        {
            walkingSounds = false;
            crouchingSounds = false;
            runningSounds = true;
            audioRunPJ.volume = 1f;
            audioWalkPJ.Stop();
            audioRunPJ.Play();
            audioRunPJ.loop = true;
        }
        else if (!runSounds && runningSounds)
        {
            runningSounds = false;
            audioRunPJ.loop = false;
            audioRunPJ.Stop();
        }
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

    private void estropearLinterna()
    {
        float valor = UnityEngine.Random.Range(0.0f, 1.0f);
        if (valor < 0.001f)
        {
            lightOn = false;
            lanternTaked = false;
            StartCoroutine(parpadeoLuz());

            if (!lantern.enabled)
            {
                lanternFail.Play();
            }

        }
    }

    IEnumerator parpadeoLuz()
    {
        lantern.enabled = false;
        yield return new WaitForSeconds(0.5f);
        lantern.enabled = true;
        yield return new WaitForSeconds(0.25f);
        lantern.enabled = false;
        yield return new WaitForSeconds(0.5f);
        lantern.enabled = true;
        yield return new WaitForSeconds(0.25f);
        lantern.enabled = false;
        yield return new WaitForSeconds(0.5f);
        lanternTaked = true;
        yield return null;
    }
}
