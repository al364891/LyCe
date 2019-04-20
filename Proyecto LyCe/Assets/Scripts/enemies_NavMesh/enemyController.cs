using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour {
    //ESTE SCRIPT: es para el radio en el que te puede "ver" el padre
    
    public float lookRadious = 10f;

    Transform target;
    NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        target = playerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadious)
        {
            agent.SetDestination(target.position);
        }
	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadious);
    }
    
}
