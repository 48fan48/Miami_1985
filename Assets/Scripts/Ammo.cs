using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{

    public GunController gun;
    private PauseMenu pauseMenuScript;

    // Start is called before the first frame update
    void Start()
    {
        //Get the menu script from the object
        pauseMenuScript = GameObject.Find("Canvas").GetComponent<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision other) {
     if (other.gameObject.CompareTag("Player")) {
         other.gameObject.GetComponent<PlayerController>().gun.AddAmmo();
         pauseMenuScript.ammoText.text = "Ammo: " + (int)other.gameObject.GetComponent<PlayerController>().gun.ammoAmount;
         Destroy(gameObject);
        }
    }
}
