using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public BulletController bullet;
    public Transform firePoint;
    public bool isFiring;
    public float bulletSpeed;
    public float timeBetweenShots;
    public float ammoAmount = 20;

    private float shotCounter;
    private PauseMenu pauseMenuScript;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = 0;
        //Get the menu script from the object
        pauseMenuScript = GameObject.Find("Canvas").GetComponent<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isFiring && shotCounter <= 0 && ammoAmount > 0) // We are firing the gun
        {
            // Reset the time between shots
            shotCounter = timeBetweenShots;  
            if (gameObject.CompareTag("PlayerGun")) {
                // Decrease the ammo by one
                ammoAmount -= 1;
                // Update UI ammo amount
                pauseMenuScript.ammoText.text = "Ammo: " + (int)ammoAmount;
            }
            // Create the bullet as a BulletController
            BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
        }
        else
        {
                shotCounter -= Time.deltaTime;
        }
    }

    public void AddAmmo()
    {
        ammoAmount += 20;
    }
}
