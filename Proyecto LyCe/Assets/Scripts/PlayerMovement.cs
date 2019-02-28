using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public bool isCrouching; //Indica si esta agachado

    private float speed; //Variable donde se guarda la velocidad de cada momento
    private float w_speed = 0.5f; //Walking speed
    private float l_speed = 0.25f; //Lateral speed
    private float r_speed = 1f; //Running speed
    private float c_speed = 0.25f; //Crouching speed
    public float rotSpeed; //Velocidad de rotacion

    Rigidbody rb;
    Animator anim;
    CapsuleCollider col_size;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        col_size = GetComponent<CapsuleCollider>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftControl)) { //Cuando pulsas el boton control izquierdo te agachas o levantas
            if (isCrouching) //Te levantas
            {
                isCrouching = false;
                anim.SetBool("Crouched", false);
                col_size.height = 2; //Modifica el tamano del collider para la nueva posicion
                col_size.center = new Vector3(0, 1, 0); //Cambia el centro del collider
            } else //Te agachas
            {
                isCrouching = true;
                anim.SetBool("Crouched", true);
                speed = c_speed; //Indica la velocidad que tendra agachado
                col_size.height = 1; //Modifica el tamano del collider para la nueva posicion
                col_size.center = new Vector3(0, 0.5f, 0); //Cambia el centro del collider
            }
        }

        var z = Input.GetAxis("Vertical") * speed; //Movimiento en z
        var x = Input.GetAxis("Horizontal") * speed; //Movimiento en x

        transform.Translate(x, 0, z); //Aplica el movimiento del personaje
        //transform.Rotate(0, 0, 0); //Para cuando se tenga que rotar en la cámara

        if (isCrouching)
        {
            //Controles agachado
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) //Si se mueve
            {
                anim.SetInteger("WalkDirection", 0);
                anim.SetFloat("Velocity", 1f);
            } else //Si esta quieto
            {
                anim.SetInteger("WalkDirection", 0);
                anim.SetFloat("Velocity", 0f);
            }
        } else if (Input.GetKey(KeyCode.LeftShift))
        {
            //Controles corriendo
            if(Input.GetKey(KeyCode.W)) //Si se mueve hacia adelante
            {
                speed = r_speed;
                anim.SetInteger("WalkDirection", 0);
                anim.SetFloat("Velocity", 6f);
            } else if (Input.GetKey(KeyCode.S)) //Si se mueve hacia detras
            {
                speed = w_speed;
                anim.SetInteger("WalkDirection", 0);
                anim.SetFloat("Velocity", -1f);
            } else if (Input.GetKey(KeyCode.A)){ //Si se mueve hacia su izquierda
                speed = l_speed;
                anim.SetInteger("WalkDirection", 1);
            } else if (Input.GetKey(KeyCode.D)){ //Si se mueve hacia su derecha
                speed = l_speed;
                anim.SetInteger("WalkDirection", 2);
            } else //Si esta quieto
            {
                anim.SetFloat("Velocity", 0f);
                anim.SetInteger("WalkDirection", 0);
            }
        } else if (!isCrouching)
        {
            speed = w_speed;
            //Controles de pie
            if (Input.GetKey(KeyCode.W)) //Si se mueve hacia adelante
            {
                anim.SetInteger("WalkDirection", 0);
                anim.SetFloat("Velocity", 1f);
            } else if (Input.GetKey(KeyCode.S)) //Si se mueve hacia detras
            {
                anim.SetInteger("WalkDirection", 0);
                anim.SetFloat("Velocity", -1f);
            } else if (Input.GetKey(KeyCode.A)) //Si se mueve hacia su izquierda
            {
                speed = l_speed;
                anim.SetInteger("WalkDirection", 1);
            }
            else if (Input.GetKey(KeyCode.D)) //Si se mueve hacia su derecha
            {
                speed = l_speed;
                anim.SetInteger("WalkDirection", 2);
            } else //Si no se mueve
            {
                anim.SetInteger("WalkDirection", 0);
                anim.SetFloat("Velocity", 0f);
            }
        }
	}
}
