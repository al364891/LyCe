using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    //ESTE SCRIPT: sistema automatizado de "puntos donde irá el enemigo"
    //simplemente crear un GameObject como punto de referencia de donde quieres ir
    //y pasarlo a lista "puntos" de este script

    public UnityEngine.AI.NavMeshAgent padre;
    public List<GameObject> puntos;
    List<Vector3> puntosUtiles;
    Vector3 puntosTemp;

    //int p;
    float posPadreX, posPadreZ;
    float posX, posZ;

    public Vector3 pDestino;
    Vector3 pAux;


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

        padre.SetDestination(pDestino);
    }
	

	void Update ()
    {
        posPadreX = padre.transform.position.x;
        posPadreZ = padre.transform.position.z;


        if ((posPadreX == posX) && (posPadreZ == posZ))
        {
            pAux = pDestino;
            puntosUtiles.RemoveAt(0);
            puntosUtiles.Add(pAux);
            pDestino = puntosUtiles[0];

            posX = pDestino.x;
            posZ = pDestino.z;

            padre.SetDestination(pDestino);

            /*Debug.Log("LISTA:"+puntosUtiles+"\n");
            for(int i=0; i<puntosUtiles.Count; i++)
            {
                Debug.Log("++++++++++++++++++");
                Debug.Log("punto["+i+"]X: " + puntosUtiles[i].x);
                Debug.Log("punto[" + i + "]Y: " + puntosUtiles[i].y);
                Debug.Log("punto[" + i + "]Z: " + puntosUtiles[i].z);
            }*/
        }
    }
}
