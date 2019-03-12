﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public bool isCrouching; //Indica si esta agachado

    private float speed; //Variable donde se guarda la velocidad de cada momento
    private float w_speed = 8f; //Walking speed
    private float l_speed = 7f; //Lateral speed
    private float r_speed = 10f; //Running speed
    private float c_speed = 5f; //Crouching speed
    public float rotSpeed; //Velocidad de rotacion
    private float gravity = 20f;
    private Vector3 moveDir;

    CharacterController controller;
    Animator anim;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
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
                }
                else //Te agachas
                {
                    isCrouching = true;
                    anim.SetBool("Crouched", true);
                    speed = c_speed; //Indica la velocidad que tendra agachado
                    controller.height = 1; //Modifica el tamano del collider para la nueva posicion
                    controller.center = new Vector3(0, 0.5f, 0); //Cambia el centro del collider
                }
            }
                
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            moveDir = moveDir * speed;
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
            else if (Input.GetKey(KeyCode.LeftShift))
            {
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
        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player has collided with " + collision.collider.name);
    }
}
