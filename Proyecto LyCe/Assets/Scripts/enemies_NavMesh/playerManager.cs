using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour {
    //ESTE SCRIPT: hay que asignarle a "quien" va a seguir cuando le vea
    #region Singleton

    public static playerManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion
    
    public GameObject player;
    
}
