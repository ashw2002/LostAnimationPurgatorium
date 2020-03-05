using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//For main menu buttons
public class MainMenuScript : MonoBehaviour
{

    //void that loads a new game
    public void StartGame()
    {
        SaveSystem.isLoaded = false;
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    //void that brings up load game mode screen
    public void LoadGame()
    {
        SaveSystem.isLoaded = true;
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    //void that brings back up main menu screen
    public void BackToMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    //void that quits the game 
    public void Quit()
    {
        Application.Quit();
    }

    //Will get used when a button is pushed in the pause menu
    public void BackToGame()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().isPaused = false;
        GameObject.FindGameObjectWithTag("Pause Screen").SetActive(false);
        Time.timeScale = 1;
    }
}
