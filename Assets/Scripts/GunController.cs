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

    private float shotCounter;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFiring && shotCounter <= 0) // We are firing the gun
        {
            shotCounter = timeBetweenShots;  // Reset the time between shots
            // Create the bullet as a BulletController
            BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
        }
        else
        {
                shotCounter -= Time.deltaTime;
        }
    }
}
