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
        //If the player went from the game to the menu, then timeScale is set to 0, reset it to 1
        if(Time.timeScale == 0f){
            Time.timeScale = 1f;
        }

        //Load the first Scene (Will be changed to load based on file input)
        SceneManager.LoadScene("Mafia 1");
    }

    //Quits the game when the player chooses the "Quit" menu item
    public void ExitGame(){
        Application.Quit();
    }
}
