using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Timer timeManager;
    public GameObject endScreen;
    public PauseMenu pauseMenu;

    public void EndGame()
    {
        pauseMenu.ToggleCanPause();
        timeManager.EndTimer();
        endScreen.SetActive(true);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
