using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu;
    [SerializeField] Image backgroundImage;
    [SerializeField] Color backgroundColor;
    Color backgroundTransparent;
    [SerializeField] float pauseTime = 0.5f;
    [SerializeField] RectTransform pauseComponents;
    [SerializeField] Transition transition;

    Vector3 pausePosition;
    bool canPause = true;

    void Start()
    {
        backgroundTransparent = backgroundColor;
        backgroundTransparent.a = 0;

        backgroundImage.color = backgroundTransparent;

        pausePosition = pauseComponents.position;
        pauseComponents.position += new Vector3(0, 1000);
    }

    void Update()
    {
        if (canPause && Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenu.activeSelf) OpenPause();
            else ClosePause();
        }
    }

    [ContextMenu("Open Pause")]
    public void OpenPause()
    {
        pauseMenu.SetActive(true);
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0, pauseTime).SetUpdate(true);
        backgroundImage.DOColor(backgroundColor, pauseTime).SetUpdate(true);
        pauseComponents.DOMove(pausePosition, pauseTime).SetUpdate(true);
    }

    [ContextMenu("Close Pause")]
    public void ClosePause()
    {
        canPause = false;
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 1, pauseTime).SetUpdate(true);
        backgroundImage.DOColor(backgroundTransparent, pauseTime).SetUpdate(true);
        pauseComponents.DOMove(pausePosition - new Vector3(0, 1000), pauseTime).SetUpdate(true);
        StartCoroutine(closePauseMenu());
    }

    public void QuitToMenu()
    {
        transition.TransitionExit("MainMenu");
    }

    private IEnumerator closePauseMenu()
    {
        yield return new WaitForSecondsRealtime(pauseTime);
        pauseComponents.position = pausePosition + new Vector3(0, 1000);
        canPause = true;
        pauseMenu.SetActive(false);
    }

}
