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
public float health = 100f;
public float timeBtwShots = 2f;
public float timeForDeath = 4f;
public float fireRadius = 5f;
public float force = 2000f;
public bool isDead = false;

private Animator animator;
private AudioSource gunShot;
private float timerShots;
private float timerDeath;
private PauseMenu pauseMenuScript;
private static float playerScore;

    // Start is called before the first frame update
    void Start()
    {
        // Get the player object 
        player = GameObject.Find("Fidel");
        playerScore = 0;
        // Get the animator
        animator = GetComponent<Animator>();
        // Get the gun shot audio source
        gunShot = GetComponent<AudioSource>();
        //Get the menu script from the object
        pauseMenuScript = GameObject.Find("Canvas").GetComponent<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the distance between the enemy and the player
        float distance = Vector3.Distance(player.transform.position, transform.position);
        
        // If the player is within a certain range
        if(distance <= fireRadius &&  health > 0) {
            ShootPlayer();
        } 

         // Destroy the enemy if the health is less than or equal to 0
        if(health <= 0) {
            isDead = true;
            UpdateScore(100);
            StartCoroutine(DeathAnimation());
        }

    }

    void UpdateScore(float val){
        playerScore += val;
        pauseMenuScript.scoreText.text = "Score: $" + (int)playerScore;
    }

    // Determines when the enemy should shoot at the player
    void ShootPlayer() 
    {
        RaycastHit hitPlayer;
        
        // Create a ray based on the player position
        Ray playerPos = new Ray(transform.position, transform.forward);

        // If the player position is in range
        if(Physics.SphereCast(playerPos, 0.25f, out hitPlayer, fireRadius))
        {

            transform.LookAt(player.transform);
            // Used so the enemy can't spam bullets; creates a time between shots
            if (timerShots <= 0 && hitPlayer.transform.tag == "Player")
            {
                // Create the bullet
                Instantiate(projectilePrefab, transform.position + new Vector3(1,1.7f,0), transform.rotation);
                // Play the gun shot
                gunShot.Play();
                // Set the timer 
                timerShots = timeBtwShots;
            }
            else 
            {
                // Decrease the time between shots
                timerShots -= Time.deltaTime;
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
        // Destroy the enemy once the animation is finished
        Destroy(gameObject);
    }

}
