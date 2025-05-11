using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

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
    [SerializeField] TMP_Text prompt;
    [SerializeField] float controlsTime = 0.5f;
    [SerializeField] Color blurColor;
    Color blurTransparent;
    Sequence promptColorSequence;

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
        logoRotation.Append(logoTransform.DORotate(new Vector3(0, 0, rotationAmount), logoAnimationTime).SetEase(Ease.InOutQuad).SetUpdate(true));
        logoRotation.Append(logoTransform.DORotate(new Vector3(0, 0, -rotationAmount), logoAnimationTime).SetEase(Ease.InOutQuad).SetUpdate(true));
        logoRotation.SetLoops(-1, LoopType.Yoyo);

        logoScale = DOTween.Sequence();
        logoScale.Append(logoTransform.DOScale(new Vector3(scaleAmount, scaleAmount, 1), logoAnimationTime / 2).SetEase(Ease.InOutQuad).SetUpdate(true));
        logoScale.Append(logoTransform.DOScale(new Vector3(1, 1, 1), logoAnimationTime / 2).SetEase(Ease.InOutQuad).SetUpdate(true));
        logoScale.SetLoops(-1, LoopType.Yoyo);

        promptColorSequence = DOTween.Sequence();
        Color promptColor = prompt.color;
        Color promptTransparent = prompt.color;
        promptTransparent.a = 0.25f;
        promptColorSequence.Append(prompt.DOColor(promptTransparent, 1).SetEase(Ease.InOutQuad).SetUpdate(true));
        promptColorSequence.Append(prompt.DOColor(promptColor, 1).SetEase(Ease.InOutQuad).SetUpdate(true));
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
        logoRotation.Kill();
        logoScale.Kill();
        transition.TransitionExit("GameScene");
    }

    [ContextMenu("Open Controls")]
    public void OpenControls()
    {
        controlsMenuOpen = true;
        controlsMenu.SetActive(true);
        controlsPanel.localScale = Vector3.zero;
        controlsPanel.DOScale(Vector3.one, controlsTime);
        controlsPanel.DOMoveY(2000, 0);
        controlsPanel.DOMoveY(700, controlsTime).SetEase(Ease.InOutQuad);
        controlsBlurImage.DOColor(blurColor, controlsTime);
        promptColorSequence.Play();
    }

    [ContextMenu("Close Controls")]
    public void CloseControls()
    {
        controlsMenuOpen = false;
        controlsMenu.SetActive(true);
        controlsPanel.DOScale(Vector3.zero, controlsTime);
        controlsBlurImage.DOColor(blurTransparent, controlsTime);
        controlsPanel.DOMoveY(2000, controlsTime).SetEase(Ease.InOutQuad);
        StartCoroutine(CloseControlsMenu());
    }

    [ContextMenu("Quit Game")]
    public void QuitGame()
    {
        DOTween.KillAll();
        Application.Quit();
    }

    private IEnumerator CloseControlsMenu()
    {
        yield return new WaitForSeconds(controlsTime);
        promptColorSequence.Kill();
        controlsMenu.SetActive(false);
    }

}
