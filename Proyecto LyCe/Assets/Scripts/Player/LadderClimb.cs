using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimb : MonoBehaviour {

    public Transform ChController;
    public bool inside = false;
    private float heightFactor = 10f;
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            playerMovement.enabled = false;
            inside = !inside;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            playerMovement.enabled = true;
            inside = !inside;
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
