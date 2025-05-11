using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Timer timeManager;
    public GameObject endScreen;
    public PauseMenu pauseMenu;

    [SerializeField] Transition transition;

    public void EndGame()
    {
        pauseMenu.ToggleCanPause();
        timeManager.EndTimer();
        endScreen.SetActive(true);
    }

    public void ResetGame()
    {
        transition.TransitionExit(SceneManager.GetActiveScene().name);
    }
}
