using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    //ESTE SCRIPT: sistema automatizado de "puntos donde irá el enemigo"
    //simplemente crear un GameObject como punto de referencia de donde quieres ir
    //y pasarlo a lista "puntos" de este script

    public int wait_seconds;
    public UnityEngine.AI.NavMeshAgent enemy;
    public List<GameObject> puntos;
    List<Vector3> puntosUtiles;
    Vector3 puntosTemp;

    //int p;
    float posPadreX, posPadreZ;
    float posX, posZ;

    public Vector3 pDestino;
    Vector3 pAux;

    bool espera = false;

    Animator anim;

    void Start ()
    {
        puntosUtiles = new List<Vector3>();

        for (int i = 0; i < puntos.Count; i++)
        {
            puntosTemp = (Vector3)puntos[i].transform.position;
            puntosUtiles.Add(puntos[i].transform.position);
        }

        pDestino = puntosUtiles[0];
        posX = pDestino.x;
        posZ = pDestino.z;

        enemy.SetDestination(pDestino);

        anim = GetComponent<Animator>();
    }
	

	void Update ()
    {
        posPadreX = enemy.transform.position.x;
        posPadreZ = enemy.transform.position.z;
            /*
            if ((posPadreX == posX) && (posPadreZ == posZ))
            {
                pAux = pDestino;
                puntosUtiles.RemoveAt(0);
                puntosUtiles.Add(pAux);
                pDestino = puntosUtiles[0];

                posX = pDestino.x;
                posZ = pDestino.z;

                padre.SetDestination(pDestino);

            }*/


            /*
            if ((posPadreX == posX) && (posPadreZ == posZ))
            {
                if (espera == false)
                {
                    StartCoroutine(Wait_enemy());
                }
                else
                {
                    //Debug.Log("bool1: " + espera);
                    espera = false;

                    pAux = pDestino;
                    puntosUtiles.RemoveAt(0);
                    puntosUtiles.Add(pAux);
                    pDestino = puntosUtiles[0];

                    posX = pDestino.x;
                    posZ = pDestino.z;

                    padre.SetDestination(pDestino);
                    //Debug.Log("bool2: " + espera);
                }
            }*/


            if (espera == false)
        {
            if ((posPadreX == posX) && (posPadreZ == posZ))
            {
                StartCoroutine(Wait_enemy());
            }
        }
        else
        {
            if (Vector3.Distance(enemy.transform.position,pDestino) <= 5)
            {
                pAux = pDestino;
                puntosUtiles.RemoveAt(0);
                puntosUtiles.Add(pAux);
                pDestino = puntosUtiles[0];

                posX = pDestino.x;
                posZ = pDestino.z;

            }
            enemy.SetDestination(pDestino);
            espera = false;
            anim.SetFloat("Speed", 7.0f);
        }
    }

    IEnumerator Wait_enemy()
    {
        anim.SetFloat("Speed", 0.0f);
        yield return new WaitForSeconds(wait_seconds);
        espera = true;
    }

}


