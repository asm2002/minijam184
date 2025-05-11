using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class Countdown : MonoBehaviour
{

    TMP_Text countdownText;
    [SerializeField] Ball ball;
    [SerializeField] Timer timer;

    void Start()
    {
        countdownText = GetComponent<TMP_Text>();
        countdownText.rectTransform.DOMoveY(-1000, 0);
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        countdownText.fontSize = 50;
        countdownText.rectTransform.DOMoveY(Screen.currentResolution.height / 2, 1.5f).SetEase(Ease.InOutQuad).SetUpdate(true);
        countdownText.SetText("Get the ball as high as you can!");
        yield return new WaitForSeconds(3);

        countdownText.fontSize = 250;
        countdownText.SetText("3");
        countdownText.rectTransform.DORotate(new Vector3(0, 0, 1), 1).SetEase(Ease.InOutQuad).SetUpdate(true);
        yield return new WaitForSeconds(1);

        countdownText.SetText("2");
        countdownText.rectTransform.DORotate(new Vector3(0, 0, -1), 1).SetEase(Ease.InOutQuad).SetUpdate(true);
        yield return new WaitForSeconds(1);

        countdownText.SetText("1");
        countdownText.rectTransform.DORotate(new Vector3(0, 0, 1), 1).SetEase(Ease.InOutQuad).SetUpdate(true);
        yield return new WaitForSeconds(1);

        countdownText.rectTransform.DORotate(new Vector3(0, 0, -1), 1f).SetEase(Ease.InOutQuad).SetUpdate(true);
        ball.startGame();
        timer.StartTimer();
        countdownText.SetText("Go!");
        Color countdownTextColor = countdownText.color;
        countdownTextColor.a = 0;
        countdownText.DOColor(countdownTextColor, 3).SetEase(Ease.InOutQuad).SetUpdate(true);
        yield return new WaitForSeconds(1.5f);

        countdownText.rectTransform.DOMoveY(1000, 1.5f).SetEase(Ease.InOutQuad).SetUpdate(true);

    }

}
