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
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
        animator.SetFloat("Horizontal", horizontalInput);

        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);
        animator.SetFloat("vertical", verticalInput);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectilePrefab, transform.position, transform.rotation);
        }

        //Needs work, but turns user based on mouse position
        Vector3 input = Input.mousePosition;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(input.x, input.y, Camera.main.transform.position.y));
        transform.LookAt(0.3f * mousePosition + Vector3.up * transform.position.y);

        if (horizontalInput == 0 && verticalInput == 0) {
            animator.SetBool("StandingStill", true);
        } else {
            animator.SetBool("StandingStill", false);
        }
    }

}
