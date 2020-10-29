/*
 * Unity AI shooting the Player // Top Down 3D Tutorial Beginner 05 by L_Sin Gularity
 * Logan Bland, John Clay, Jalen Wayt
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
public GameObject player;
public GameObject projectilePrefab;

private float timerShots;
public float timeBtwShots = 0.25f;

public float fireRadius = 5f;
public float force = 2000f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Fidel");
    }

    // Update is called once per frame
    void Update()
    {
        // Get the distance between the enemy and the player
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // If the player is within a certain range
        if(distance <= fireRadius) {
            ShootPlayer();
        } 
    }

    void ShootPlayer() 
    {
        RaycastHit hitPlayer;
        Ray playerPos = new Ray(transform.position, transform.forward);

        if(Physics.SphereCast(playerPos, 0.25f, out hitPlayer, fireRadius))
        {
            if (timerShots <= 0 && hitPlayer.transform.tag == "Player")
            {
                Instantiate(projectilePrefab, transform.position + new Vector3(1,0,0), transform.rotation);
                timerShots = timeBtwShots;
            }
            else 
            {
                timerShots -= Time.deltaTime;
            }
        }
    } 
    
}
