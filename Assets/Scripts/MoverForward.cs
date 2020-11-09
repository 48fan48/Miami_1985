using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverForward : MonoBehaviour
{
    public float speed = 40;
    public float damage = 20;
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
        if(other.gameObject.CompareTag("Enemy")){
            // Destroy the bullet
            Destroy(gameObject);
            // Get the enemy script
            enemyScript = other.gameObject.GetComponent<Enemy>();
            // Lower the enemy health
            enemyScript.updateHealth(damage);
        }

        // If the bullet hits the player
        if(other.gameObject.CompareTag("Player")){
            // Destroy the bullet
            Destroy(gameObject);
            // Get the player controller script
            playerScript = other.gameObject.GetComponent<PlayerController>();
            // Decrease the player health by 5
            playerScript.decreaseHealth(5);
        }
    }
}
