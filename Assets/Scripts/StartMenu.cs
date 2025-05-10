using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] GameObject buttons;
    [SerializeField] float selectTime = 0.25f;
    [SerializeField] Vector2 buttonSize = new Vector2(100, 30);
    [SerializeField] float growthAmount = 1.05f;

    [Header("Controls Menu")]
    [SerializeField] GameObject controlsMenu;
    [SerializeField] Image controlsBlurImage;
    [SerializeField] RectTransform controlsPanel;
    [SerializeField] float controlsTime = 0.5f;
    [SerializeField] Color blurColor;
    Color blurTransparent;

    [Space]
    [SerializeField] Transition transition;

    bool controlsMenuOpen = false;

    private void Start()
    {
        blurTransparent = blurColor;
        blurTransparent.a = 0;
        controlsPanel.localScale = Vector3.zero;
        controlsBlurImage.color = blurTransparent;
    }

    private void Update()
    {
        if (controlsMenuOpen && (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(0))) CloseControls();
    }

    public void ButtonSelected(GameObject button)
    {
        RectTransform transform = button.GetComponent<RectTransform>();
        Image image = button.GetComponent<Image>();
        transform.DOSizeDelta(buttonSize * growthAmount, selectTime);
    }

    public void ButtonDeselected(GameObject button)
    {
        RectTransform transform = button.GetComponent<RectTransform>();
        Image image = button.GetComponent<Image>();
        transform.DOSizeDelta(buttonSize, selectTime);
    }

    [ContextMenu("Start Game")]
    public void StartGame()
    {
        transition.TransitionExit("GameScene");
    }

    [ContextMenu("Open Controls")]
    public void OpenControls()
    {
        controlsMenuOpen = true;
        controlsMenu.SetActive(true);
        controlsPanel.localScale = Vector3.zero;
        controlsPanel.DOScale(Vector3.one, controlsTime);
        controlsBlurImage.DOColor(blurColor, controlsTime);
    }

    [ContextMenu("Close Controls")]
    public void CloseControls()
    {
        controlsMenuOpen = false;
        controlsMenu.SetActive(true);
        controlsPanel.DOScale(Vector3.zero, controlsTime);
        controlsBlurImage.DOColor(blurTransparent, controlsTime);
        StartCoroutine(CloseControlsMenu());
    }

    [ContextMenu("Quit Game")]
    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator CloseControlsMenu()
    {
        yield return new WaitForSeconds(controlsTime);
        controlsMenu.SetActive(false);
    }

}
