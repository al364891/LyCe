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

    public float viewRadius;
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();

    public PlayerMovement playerMovement;
	// Use this for initialization
	void Start ()
    {
        IA = enemy.GetComponent<IA>();
        life_player = player.GetComponent<life_player>();

        target = playerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();

        anim = GetComponent<Animator>();

        viewRadius = 18;
        viewAngle = 60;
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(target.position, transform.position);
        FindVisibleTargets();

        if (playerMovement.isCrouching)
        {
            viewRadius = 10;
        }
        else if (playerMovement.isRunning)
        {
            viewRadius = 26;
        }
        else
        {
            viewRadius = 18;
        }

        if (visibleTargets.Count>0)
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

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for(int i=0; i<targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
        
        Vector3 viewAngleA = DirFromAngle(-viewAngle/2, false);
        Vector3 viewAngleB = DirFromAngle(viewAngle / 2, false);

        Gizmos.DrawLine(transform.position, transform.position + viewAngleA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * viewRadius);
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
