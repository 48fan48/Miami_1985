using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAid : MonoBehaviour
{
    private PauseMenu pauseMenuScript;
    
    // Start is called before the first frame update
    void Start()
    {
        pauseMenuScript = GameObject.Find("Canvas").GetComponent<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision other)
    {
        // If the player runs into the first aid
        if (other.gameObject.CompareTag("Player"))
        {
            // Get the player controller script and increase the health
            other.gameObject.GetComponent<PlayerController>().increaseHealth();
            // Destroy the health pack
            Destroy(gameObject);
        }
    }
}
