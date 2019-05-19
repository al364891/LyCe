using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargarPartida : MonoBehaviour {

    public Inventory inventory;
    public PlayerMovement playerMovement;

    public GameObject banyo2Key, childBoxKey, ganchoKey, tenazasKey, ganzuaKey, axeKey, boxSalonKey, boxBuhardillaKey, palancaKey, lantern, salonBoxKey, acidoKey, pasilloChildKey, escaleras;


	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt("banyo2Planta2Key") == 1)
        {
            Destroy(banyo2Key);
            inventory.hasBanyo2Planta2Key = true;
        }
        if (PlayerPrefs.GetInt("childBoxKey") == 1)
        {
            Destroy(childBoxKey);
            inventory.hasChildBoxKey = true;
        }
        if (PlayerPrefs.GetInt("ganchoKey") == 1)
        {
            Destroy(ganchoKey);
            inventory.hasGanchoKey = true;
        }
        if (PlayerPrefs.GetInt("tenazasKey") == 1)
        {
            Destroy(tenazasKey);
            inventory.hasTenazasKey = true;
            escaleras.GetComponent<Doors>().controlLadder();
        }
        if (PlayerPrefs.GetInt("ganzuaKey") == 1)
        {
            Destroy(ganzuaKey);
            inventory.hasGanzuaKey = true;
        }
        if (PlayerPrefs.GetInt("axeKey") == 1)
        {
            Destroy(axeKey);
            inventory.hasAxeKey = true;
        }
        if (PlayerPrefs.GetInt("boxSalonKey") == 1)
        {
            Destroy(boxSalonKey);
            inventory.hasBoxSalonKey = true;
        }
        if (PlayerPrefs.GetInt("boxBuhardillaKey") == 1)
        {
            Destroy(boxBuhardillaKey);
            inventory.hasBoxBuhardillaKey = true;
        }
        if (PlayerPrefs.GetInt("palancaKey") == 1)
        {
            Destroy(palancaKey);
            inventory.hasPalancaKey = true;
        }
        if (PlayerPrefs.GetInt("lantern") == 1)
        {
            Destroy(lantern);
            playerMovement.lanternTaked = true;
        }
        if (PlayerPrefs.GetInt("salonBoxKey") == 1)
        {
            Destroy(salonBoxKey);
            inventory.hasSalonBoxKey = true;
        }
        if (PlayerPrefs.GetInt("acidoKey") == 1)
        {
            Destroy(acidoKey);
            inventory.hasAcidoKey = true;
        }
        if (PlayerPrefs.GetInt("pasilloChildKey") == 1)
        {
            Destroy(pasilloChildKey);
            inventory.hasPasilloChildKey = true;
        }
	}
}
