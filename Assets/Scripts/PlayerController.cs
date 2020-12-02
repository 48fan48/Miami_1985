using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject gameOverMenu;
    public PauseMenu pauseMenuScript;
    public GunController gun;
    public float horizontalInput;
    public float verticalInput;
    public float speed = 3;
    public float health = 100f;
    public bool gameIsOver = false;
    
    private Animator animator;
    private Camera mainCamera;
    // private AudioSource gunShot;

    // Start is called before the first frame update
    void Start()
    {
        // Get the player animator
        animator = GetComponent<Animator>();
        // Get the main camera
        mainCamera = FindObjectOfType<Camera>();
        // Get the gunshot audio
        // gunShot = GetComponent<AudioSource>();
        // Get the Pause Menu script
        pauseMenuScript = GameObject.Find("Canvas").GetComponent<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {

        // Used for player movement 
        AltMove();
        LookAtMouse();
        
        // Check the health, if it is less than zero end the game
        if(health <= 0){
            GameOver();
        }
    }

    // Player movement
    public void AltMove()
    {

        if (Input.GetKey(KeyCode.D)) 
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
            animator.SetBool("Move Right", true);
        } 
        if (Input.GetKeyUp(KeyCode.D)) 
        {
            animator.SetBool("Move Right", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.back* speed * Time.deltaTime;
            animator.SetBool("Move Left", true);
        } 
        if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("Move Left", false);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            animator.SetBool("Move Forward", true);
        }
        if (Input.GetKeyUp(KeyCode.W)) 
        {
            animator.SetBool("Move Forward", false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            animator.SetBool("Move Backwards", true);
        }
        if (Input.GetKeyUp(KeyCode.S)) 
        {
            animator.SetBool("Move Backwards", false);
        }
        if (Input.GetMouseButtonDown(0) && Time.deltaTime != 0)
        {
            gun.isFiring = true;
            // gunShot.Play();
        }
        if (Input.GetMouseButtonUp(0))
        {
            gun.isFiring = false;
        }
    }

    // Decrease the player health
    public void decreaseHealth(int healthDecrease)
    {
        health -= healthDecrease;
        // Display the player health
        pauseMenuScript.healthText.text = "Health: " + health;
    }

    // Increase the player health
    public void increaseHealth()
    {
        health = 100;
        // Display the player health
        pauseMenuScript.healthText.text = "Health: " + health;
    }

    public void GameOver()
    {
        // Start the death animation
        animator.SetBool("Is Dead", true);
        // Display the game over screen
        gameOverMenu.SetActive(true);
        // Set the game over to true
        gameIsOver = true;
        // Pause the game
        Time.timeScale = 0f;
    }

    public void LookAtMouse()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        // If the game is not paused
        if(Time.deltaTime != 0)
        {
            if(groundPlane.Raycast(cameraRay , out rayLength))
            {
                Vector3 lookPoint = cameraRay.GetPoint(rayLength);
                Debug.DrawLine(cameraRay.origin, lookPoint, Color.blue);
                transform.LookAt(new Vector3(lookPoint.x, transform.position.y, lookPoint.z));
            }
        }

    }

}
