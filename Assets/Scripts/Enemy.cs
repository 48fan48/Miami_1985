/*
 * Unity AI shooting the Player // Top Down 3D Tutorial Beginner 05 by L_Sin Gularity
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

public GameObject player;
public GameObject projectilePrefab;
public GunController gun;
public float health = 100f;
public float timeBtwShots = 2f;
public float timeForDeath = 4f;
public float fireRadius = 5.0f;
public float force = 2000f;
public bool isDead = false;
public bool playerInRange = false;

private Animator animator;
private AudioSource gunShot;
private PauseMenu pauseMenuScript;
private Patrol patrolScript;
private float timerShots;
private float timerDeath;
private static float playerScore;

    // Start is called before the first frame update
    void Start()
    {
        // Get the player object 
        player = GameObject.Find("Fidel");
        // Get the animator
        animator = GetComponent<Animator>();
        // Get the gun shot audio source
        gunShot = GetComponent<AudioSource>();
        //Get the menu script from the object
        pauseMenuScript = GameObject.Find("Canvas").GetComponent<PauseMenu>();
        //Call this to update score on new scene for completing the previous level
        UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        // Get the distance between the enemy and the player
        float distance = Vector3.Distance(player.transform.position, transform.position);
        
        // If the player is within a certain range
        if(distance <= fireRadius &&  health > 0) {
            playerInRange = true;
            ShootPlayer();
        } 
        // The player is not within range
        else {
            playerInRange = false;
        }

         // Destroy the enemy if the health is less than or equal to 0
        if(health <= 0) {
            isDead = true;
            gun.isFiring = false;
            StartCoroutine(DeathAnimation());
        }

    }
    //Update the score with the specified value
    public void UpdateScore(float val)
    {
        // Increase the player score
        playerScore += val;
        // Display the player score
        pauseMenuScript.scoreText.text = "Score: $" + (int)playerScore;
    }

    // Determines when the enemy should shoot at the player
    void ShootPlayer() 
    {
        
        // Look at the player
        transform.LookAt(player.transform);
        
        RaycastHit hitPlayer;
        
        // Create a ray based on the player position
        Ray playerPos = new Ray(transform.position, transform.forward);

        // If the player position is in range
        if(Physics.SphereCast(playerPos, 0.25f, out hitPlayer, fireRadius))
        {
            transform.LookAt(player.transform);
            // If the ray is hitting the player, shoot at him
            if (hitPlayer.transform.tag == "Player")
            {
                // Set the shooting animation to true
                animator.SetBool("isShooting", true);
                // Stop the walking animation
                animator.SetBool("WalkingForward", false);
                // Set the gun script's variable is firing to true
                gun.isFiring = true;
            }
        }
    } 
    
    // Used to lower the enemy health
    public void updateHealth(float damage)
    {
        // Lower the health by the damage
        health = health - damage;

    }

    // Used to display the death animation
    public IEnumerator DeathAnimation()
    {
        // Start the death animation
        animator.SetBool("isDead", true);
        // Wait till the animation finshes
        yield return new WaitForSeconds(4.3f);
        //Update the score after the enemy has died
        UpdateScore(100);
        //Decrease the number of enemies
        pauseMenuScript.decreaseNumEnemies();
        // Destroy the enemy once the animation is finished
        Destroy(gameObject);
    }

}
