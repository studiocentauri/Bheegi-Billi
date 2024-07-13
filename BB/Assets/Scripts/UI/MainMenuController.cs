using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public static int level;
    // public GameObject mainMenuPanel;
    //public static bool isMainMenuActive;

    public Animator transition;

    public void OpenMenu()
    {
        // Load the main game scene (assuming it's at build index 1)
        StartCoroutine(LoadLevel(1));
    }

    public void OpenLevels()
    {
        // Load the main game scene (assuming it's at build index 1)
        StartCoroutine(LoadLevel(2));
    }

    public void OpenTutorials()
    {
        // Load the main game scene (assuming it's at build index 1)
        StartCoroutine(LoadLevel(3));
    }

    public void OpenLeaderboard()
    {
        // Load the main game scene (assuming it's at build index 1)
        StartCoroutine(LoadLevel(4));
    }

    public void OpenSettings()
    {
        // Load the main game scene (assuming it's at build index 1)
        StartCoroutine(LoadLevel(5));
       
    }

    public void OpenName()
    {
        // Load the main game scene (assuming it's at build index 1)
        StartCoroutine(LoadLevel(6));
    }

    public void OpenLevel1()
    {
        // Load the main game scene (assuming it's at build index 1)
        level = 1;
        StartCoroutine(LoadLevel(7));
    }

    public void OpenLevel2()
    {
        // Load the main game scene (assuming it's at build index 1)
        level = 2;
        StartCoroutine(LoadLevel(8));
    }

    public void OpenLevel3()
    {
        level = 3;
        StartCoroutine(LoadLevel(9));
    }

    public void Retry()
    {
        if(level == 1)
            StartCoroutine(LoadLevel(7));
        if(level == 2)
           StartCoroutine(LoadLevel(8));
        if(level == 3)
            StartCoroutine(LoadLevel(9));
    }

    public void OpenStory()
    {
        StartCoroutine(LoadLevel(10));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        // Play animation
        transition.SetTrigger("Start");
        // Wait
        yield return new WaitForSeconds(0.5f);
        // Load scene
        SceneManager.LoadScene(levelIndex);
    }

    public void QuitGame()
    {
        // Quit the game (only works in standalone builds)
        Debug.Log("QUIT");
        Application.Quit();
    }
}