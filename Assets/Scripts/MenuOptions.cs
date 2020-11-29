using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuOptions : MonoBehaviour
{
    public GameObject statsMenu;
    public TextMeshProUGUI statsText;
    Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
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
        SceneManager.LoadScene(scene.buildIndex + 1);
    }

    //Quits the game when the player chooses the "Quit" menu item
    public void ExitGame(){
        Application.Quit();
    }

    //Loads the data from the playerprefs information
    public void LoadData(){
        statsMenu.SetActive(true);
        if(PlayerPrefs.HasKey("PlayerScoreString")){
            string getStats = PlayerPrefs.GetString("PlayerScoreString");
            statsText.text = "Scores\n" + getStats;
        }else{
            statsText.text = "Welcome New Player!";
            statsText.alignment = TextAlignmentOptions.Top;
        }
    }
}
