/*
 * Unity Top Down Shooter #2 - Guns by gamesplusjames on youtube
 * https://www.youtube.com/watch?v=JVibUZugFAQ
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public float enemyDamage = 20f;
    public int playerDamage = 5;
    
    private Enemy enemyScript;
    private PlayerController playerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        
    }

    // When the bullet collides with the enemy
    void OnTriggerEnter(Collider other)
    {
        // If the bullet hits an enemy
        if(other.gameObject.CompareTag("Enemy")) {
            // Destroy the bullet
            Destroy(gameObject);
            // Get the enemy script
            enemyScript = other.gameObject.GetComponent<Enemy>();
            // Lower the enemy health
            enemyScript.updateHealth(enemyDamage);
        }

        // If the bullet hits the player
        if(other.gameObject.CompareTag("Player")) {
            // Destroy the bullet
            Destroy(gameObject);
            // Get the player controller script
            playerScript = other.gameObject.GetComponent<PlayerController>();
            // Decrease the player health by 5
            playerScript.decreaseHealth(playerDamage);
        } 
    }
}
