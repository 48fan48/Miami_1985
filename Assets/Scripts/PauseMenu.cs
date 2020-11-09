using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool IsPaused;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    private AudioSource [] allAudioSource;
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

    void Pause(){
        pauseMenu.SetActive(true);
        foreach(AudioSource audio in allAudioSource){
            audio.Stop();
        }
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void Resume(){
        pauseMenu.SetActive(false);
        foreach(AudioSource audio in allAudioSource){
            audio.Play();
        }
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void goToMenu(){
        SceneManager.LoadScene("Menu");
    }

    public void quit(){
        Application.Quit();
    }
}
