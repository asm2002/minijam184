using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartMenu : MonoBehaviour
{

    [Header("Logo")]
    [SerializeField] RectTransform logoTransform;
    [SerializeField] float logoAnimationTime = 5;
    [SerializeField] float rotationAmount = 5;
    [SerializeField] float scaleAmount = 1.1f;
    Sequence logoRotation;
    Sequence logoScale;

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

        logoRotation = DOTween.Sequence();
        logoRotation.Append(logoTransform.DORotate(new Vector3(0, 0, rotationAmount), logoAnimationTime).SetEase(Ease.InOutQuad));
        logoRotation.Append(logoTransform.DORotate(new Vector3(0, 0, -rotationAmount), logoAnimationTime).SetEase(Ease.InOutQuad));
        logoRotation.SetLoops(-1, LoopType.Yoyo);

        logoScale = DOTween.Sequence();
        logoScale.Append(logoTransform.DOScale(new Vector3(scaleAmount, scaleAmount, 1), logoAnimationTime / 2).SetEase(Ease.InOutQuad));
        logoScale.Append(logoTransform.DOScale(new Vector3(1, 1, 1), logoAnimationTime / 2).SetEase(Ease.InOutQuad));
        logoScale.SetLoops(-1, LoopType.Yoyo);

        logoRotation.Play();
        logoScale.Play();

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
