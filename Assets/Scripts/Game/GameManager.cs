using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Timer timeManager;
    public GameObject endScreen;
    public PauseMenu pauseMenu;

    [SerializeField] Transition transition;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
