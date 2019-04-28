using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour {
    //ESTE SCRIPT: es para el radio en el que te puede "ver" el padre

    IA IA;
    public GameObject enemy;
    life_player life_player;
    public GameObject player;

    public float lookRadious = 10f;
    public int damage;
    public int wait_damage_seconds;
    bool salida = true;

    Transform target;
    NavMeshAgent agent;

    [HideInInspector] public Animator anim;

	// Use this for initialization
	void Start ()
    {
        IA = enemy.GetComponent<IA>();
        life_player = player.GetComponent<life_player>();

        target = playerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();

        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadious)
        {
            if(distance > 5)
            {
                if (agent.stoppingDistance == 5){
                    agent.stoppingDistance = 0;
                }
                agent.SetDestination(target.position);
            }
            else
            {
                agent.stoppingDistance = 5;

                if (life_player.life >= 0)
                {
                    if (salida)
                    {
                        //Damage();
                        StartCoroutine(Damage_enemy());
                        Debug.Log("vida: " + life_player.life);
                    }
                }
                else
                {
                    Debug.Log("has perdido");
                }
            }
            
        }
        else
        {
            IA.enemy.SetDestination(IA.pDestino);
        }
	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadious);
    }
    
    IEnumerator Damage_enemy()
    {
        life_player.life -= damage;
        life_player.changeDamageImage();
        salida = false;
        anim.SetBool("Attacking", true);
        yield return new WaitForSeconds(wait_damage_seconds);
        anim.SetBool("Attacking", false);
        salida = true;
    }
}
