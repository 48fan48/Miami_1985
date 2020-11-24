using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Loads the game when the player chooses the "Start" menu item
    public void PlayGame(){
        if(Time.timeScale == 0f){
            Time.timeScale = 1f;
        }
        
        SceneManager.LoadScene("Mafia 1");
    }

    //Quits the game when the player chooses the "Quit" menu item
    public void ExitGame(){
        Application.Quit();
    }
}
