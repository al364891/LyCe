using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimb : MonoBehaviour {

    public AudioSource audioLadder;

    public Transform ChController;
    public bool inside = false;
    private float heightFactor = 10f;
    private PlayerMovement playerMovement;
    private bool animacion;

    Animator anim;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            playerMovement.enabled = false;
            inside = !inside;
            if (!animacion)
            {
                animacion = true;
                audioLadder.loop = true;
                playerMovement.audioWalkPJ.Stop();
                playerMovement.audioRunPJ.Stop();
                audioLadder.Play();
                
                playerMovement.anim.SetBool("Ladder", animacion);
            }
        }
        else
        {
            audioLadder.Stop();
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            playerMovement.enabled = true;
            inside = !inside;
            animacion = false;
            playerMovement.anim.SetBool("Ladder", animacion);
        }
    }

    private void Update()
    {
        if(inside==true && Input.GetKey(KeyCode.W))
        {
            ChController.transform.position += Vector3.up / heightFactor;
        }
        else if (inside == true && Input.GetKey(KeyCode.S))
        {
            ChController.transform.position += Vector3.down / heightFactor;
        }
    }
}
