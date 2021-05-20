using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour {
    public GameObject MainMenu;
    public GameObject ExitConfirmation;
    public void Play () {
        var currBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currBuildIndex+1);
    }

    public void showConfirmation () {
        MainMenu.SetActive(false);
        ExitConfirmation.SetActive(true);
    }

    public void hideConfirmation () {
        MainMenu.SetActive(true);
        ExitConfirmation.SetActive(false);
    }

    public void Exit () {
        Application.Quit();
    }
}