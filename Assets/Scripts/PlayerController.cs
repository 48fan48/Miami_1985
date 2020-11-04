using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public float speed = 3;
    public GameObject projectilePrefab;
    private Animator animator;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        mainCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //horizontalInput = Input.GetAxis("Horizontal");
        //transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
        // animator.SetFloat("Horizontal", horizontalInput);

        //verticalInput = Input.GetAxis("Vertical");
        // transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);
        // animator.SetFloat("vertical", verticalInput);
        altMove();
        shoot();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectilePrefab, transform.position, transform.rotation);
        }

        //Needs work, but turns user based on mouse position
        /*
        Vector3 input = Input.mousePosition;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(input.x, input.y, Camera.main.transform.position.y));
        transform.LookAt(0.3f * mousePosition + Vector3.up * transform.position.y);*/
        /* if(Input.GetAxis("Mouse X") < 0){
            transform.Rotate(Vector3.up * -5);
         }

         if(Input.GetAxis("Mouse X") > 0){
             transform.Rotate(Vector3.up * 5);
         }

         if (horizontalInput == 0 && verticalInput == 0) {
             animator.SetBool("StandingStill", true);
         } else {
             animator.SetBool("StandingStill", false);
         }
         */

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
    }

    public void altMove()
    {

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.back* speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }

    public void shoot()
    {
        if (Input.GetMouseButtonDown(0) && Time.deltaTime != 0)
        {
            Instantiate(projectilePrefab, transform.position, transform.rotation);
        }
    }

}
