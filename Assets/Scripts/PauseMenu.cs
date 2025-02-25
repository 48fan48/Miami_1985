﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using System;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    public GameObject [] enemyArr;
    public bool IsPaused;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject scoreMenu;
    public PlayerController playerController;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI ammoText;
    public GameObject completionMenu;
    public int numEnemies;
    public Enemy enemyScript;

    private AudioSource [] allAudioSource;
    private String saveStats;
    
    // Start is called before the first frame update
    void Start()
    {
        //Assign the enemy script variable to the enemy.cs script from the enemies
        if (SceneManager.GetActiveScene().buildIndex == 1){
            enemyScript = GameObject.Find("Enemy (Mafia 1)").GetComponent<Enemy>();
        }else if (SceneManager.GetActiveScene().buildIndex == 2){
            enemyScript = GameObject.Find("Enemy (Mafia 2)").GetComponent<Enemy>();
        }else {
            enemyScript = GameObject.Find("Enemy (Mafia 3)").GetComponent<Enemy>();
        }
        
        allAudioSource = FindObjectsOfType<AudioSource>();
        enemyArr = GameObject.FindGameObjectsWithTag("Enemy");
        numEnemies = enemyArr.Length;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //List of all the enemies within the scene
        //If the escape button is pressed, pause the game until the user unpauses or quit
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(IsPaused){
                Resume();
            }else{
                Pause();
            }
        }

        //Once there are zero enemmies left, display the completion menu
        if(numEnemies <= 0){
            Time.timeScale = 0f;
            completionMenu.SetActive(true);
            if(allAudioSource != null){
                foreach(AudioSource audio in allAudioSource){
                    if(audio != null){
                        audio.Stop();
                    }
                }
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
                if(audio != null){
                     audio.Stop();
                }
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
            if(audio != null){
                audio.Play();
            }
        }
        //Start all movement
        Time.timeScale = 1f;
        IsPaused = false;
    }

    //Go back to the menu from the pause menu, timeScale is still set to zero
    public void goToMenu(){
        SceneManager.LoadScene("Menu");
    }

    //Quit the game
    public void quit(){
        Application.Quit();
    }

    //Save the game information
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

    //Proceed to the next level
    public void NextLevel(){
        //If the scene is the last level, then end the game
        if(SceneManager.GetActiveScene().buildIndex >= 3){
            quit();
        }else{//If there are still more levels, go to the next one
            Time.timeScale = 1f;
            completionMenu.SetActive(false); 
            //Add 1000 to the score when the user proceeds to the next level 
            enemyScript.UpdateScore(1000); 
            //Load the next scene in the build index list
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    
    //Decrease number of enemies
    public void decreaseNumEnemies(){
        numEnemies--;
        Debug.Log("Num enemies: " + numEnemies);
    }
}
