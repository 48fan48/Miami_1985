using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using System;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    public bool IsPaused;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject scoreMenu;
    private AudioSource [] allAudioSource;
    public PlayerController playerController;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    private String saveStats;
    // Start is called before the first frame update
    void Start()
    {
        allAudioSource = FindObjectsOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(IsPaused){
                Resume();
            }else{
                Pause();
            }
        }
    }

    //Opens the pause menu
    void Pause(){
        //scoreMenu.SetActive(false);
        //If the game is over, do not allow the user to pause the game
        if(!playerController.gameIsOver){
            pauseMenu.SetActive(true);
            //Stop all audio sources
            foreach(AudioSource audio in allAudioSource){
                audio.Stop();
            }
            //Stop all movement
            Time.timeScale = 0f;
            IsPaused = true;
        }
    }

    public void Resume(){
       // scoreMenu.SetActive(true);
        pauseMenu.SetActive(false);
        //Start all audio sources
        foreach(AudioSource audio in allAudioSource){
            audio.Play();
        }
        //Start all movement
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void goToMenu(){
        SceneManager.LoadScene("Menu");
    }

    public void quit(){
        Application.Quit();
    }

    public void SaveGame(){
        PlayerPrefs.SetString("PlayerScore", scoreText.text);
        PlayerPrefs.SetInt("PlayerLevel", SceneManager.GetActiveScene().buildIndex);
        saveStats = "Last Score: " + scoreText.text + ", Level: " + SceneManager.GetActiveScene().buildIndex;

        if(PlayerPrefs.HasKey("PlayerScore")){
            string oldStats = PlayerPrefs.GetString("PlayerScoreString");
            saveStats = saveStats + "\n" + oldStats;
            PlayerPrefs.SetString("PlayerScoreString", saveStats);
        }else{
            PlayerPrefs.SetString("PlayerScoreString", saveStats);
        }
        PlayerPrefs.Save();
    }

    public void NextLevel(){
        //If the scene is the last level, then end the game
        if(SceneManager.GetActiveScene().buildIndex >= 3){
            quit();
        }else{//If there are still more levels, go to the next one
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    
}
