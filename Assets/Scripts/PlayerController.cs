using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public float speed = 3;
    //public GameObject projectilePrefab;
    private Animator animator;
    private Camera mainCamera;
    private AudioSource gunShot;
    public float health = 100f;
    public GameObject gameOverMenu;
    public bool gameIsOver = false;
    public PauseMenu pauseMenuScript;
    public GunController gun;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        mainCamera = FindObjectOfType<Camera>();
        gunShot = GetComponent<AudioSource>();
        pauseMenuScript = GameObject.Find("Canvas").GetComponent<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {

        altMove();
        //shoot();

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if(Time.deltaTime != 0){
            if(groundPlane.Raycast(cameraRay , out rayLength))
            {
                Vector3 lookPoint = cameraRay.GetPoint(rayLength);
                Debug.DrawLine(cameraRay.origin, lookPoint, Color.blue);
                transform.LookAt(new Vector3(lookPoint.x, transform.position.y, lookPoint.z));
            }
        }

        if(health <= 0){
            GameOver();
        }
    }

    public void altMove()
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
            gunShot.Play();
        }
        if (Input.GetMouseButtonUp(0))
        {
            gun.isFiring = false;
        }
    }

    public void decreaseHealth(int healthDecrease){
        health -= healthDecrease;
        pauseMenuScript.healthText.text = "Health: " + health;
    }

    public void increaseHealth()
    {
        health = 100;
        pauseMenuScript.healthText.text = "Health: " + health;
    }

    public void GameOver(){
        // Start the death animation
        animator.SetBool("Is Dead", true);
        gameOverMenu.SetActive(true);
        gameIsOver = true;
        Time.timeScale = 0f;
    }

}
