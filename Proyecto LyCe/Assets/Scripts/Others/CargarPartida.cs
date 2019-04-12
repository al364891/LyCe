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
        else if (PlayerPrefs.GetInt("childBoxKey") == 1)
        {
            Destroy(childBoxKey);
            inventory.hasChildBoxKey = true;
        }
        else if (PlayerPrefs.GetInt("ganchoKey") == 1)
        {
            Destroy(ganchoKey);
            inventory.hasGanchoKey = true;
        }
        else if (PlayerPrefs.GetInt("tenazasKey") == 1)
        {
            Destroy(tenazasKey);
            inventory.hasTenazasKey = true;
            escaleras.GetComponent<Doors>().controlLadder();
        }
        else if (PlayerPrefs.GetInt("ganzuaKey") == 1)
        {
            Destroy(ganzuaKey);
            inventory.hasGanzuaKey = true;
        }
        else if (PlayerPrefs.GetInt("axeKey") == 1)
        {
            Destroy(axeKey);
            inventory.hasAxeKey = true;
        }
        else if (PlayerPrefs.GetInt("boxSalonKey") == 1)
        {
            Destroy(boxSalonKey);
            inventory.hasBoxSalonKey = true;
        }
        else if (PlayerPrefs.GetInt("boxBuhardillaKey") == 1)
        {
            Destroy(boxBuhardillaKey);
            inventory.hasBoxBuhardillaKey = true;
        }
        else if (PlayerPrefs.GetInt("palancaKey") == 1)
        {
            Destroy(palancaKey);
            inventory.hasPalancaKey = true;
        }
        else if (PlayerPrefs.GetInt("lantern") == 1)
        {
            Destroy(lantern);
            playerMovement.lanternTaked = true;
        }
        else if (PlayerPrefs.GetInt("salonBoxKey") == 1)
        {
            Destroy(salonBoxKey);
            inventory.hasSalonBoxKey = true;
        }
        else if (PlayerPrefs.GetInt("acidoKey") == 1)
        {
            Destroy(acidoKey);
            inventory.hasAcidoKey = true;
        }
        else if (PlayerPrefs.GetInt("pasilloChildKey") == 1)
        {
            Destroy(pasilloChildKey);
            inventory.hasPasilloChildKey = true;
        }
	}
}
