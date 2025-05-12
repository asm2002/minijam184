using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Timer timeManager;
    public GameObject endScreen;
    public PauseMenu pauseMenu;

    [SerializeField] private Image fadeImage;
    [SerializeField] private RectTransform endScreenComponents;
    [SerializeField] private float endTime = 1.5f;
    [SerializeField] private AudioSource music;

    [SerializeField] Transition transition;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Ball ball;

    private bool gameOver  = false;

    Color fadeColor;
    Color fadeTransparent;

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

    private void Start()
    {
        fadeColor = fadeImage.color;
        fadeTransparent = fadeColor;
        fadeTransparent.a = 0;

        fadeImage.color = fadeTransparent;
    }

    public void EndGame()
    {
        if (!gameOver)
        {
            gameOver = true;
            pauseMenu.ToggleCanPause();
            timeManager.EndTimer();
            int finalscore = Mathf.RoundToInt(ball.score);
            Debug.Log("Finalscore;" + finalscore);
            scoreText.text = finalscore.ToString();

            endScreen.SetActive(true);
            fadeImage.DOColor(fadeColor, endTime).SetEase(Ease.Linear).SetUpdate(true);
            music.DOPitch(0, endTime).SetEase(Ease.Linear).SetUpdate(true);
            endScreenComponents.DOMove(endScreenComponents.position + new Vector3(0, 1000, 0), 0).SetEase(Ease.InOutQuad).SetUpdate(true);
            endScreenComponents.DOMove(endScreenComponents.position, endTime).SetUpdate(true);
        }
    }

    public void ResetGame()
    {
        transition.TransitionExit(SceneManager.GetActiveScene().name);
    }
}
