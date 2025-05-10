using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Collections;

public class Transition : MonoBehaviour
{

    [SerializeField] RectTransform transitionTransform;
    [SerializeField] Image transitionImage;
    [SerializeField] float transitionTime = 0.5f;
    [SerializeField] float distance = 1000;

    void Start()
    {
        transitionImage.enabled = true;
        TransitionEnter();
    }

    public void TransitionEnter()
    {
        Sequence enterSequence = DOTween.Sequence();
        enterSequence.Append(transitionTransform.DOLocalMoveX(distance, transitionTime).SetEase(Ease.InOutQuad));
        enterSequence.Append(transitionTransform.DOLocalMoveX(-distance, 0));
        enterSequence.Play();
    }

    public void TransitionExit(string nextScene)
    {
        transitionTransform.DOLocalMoveX(0, transitionTime).SetEase(Ease.InOutQuad);
        StartCoroutine(ExitScene(nextScene));
    }

    private IEnumerator ExitScene(string nextScene)
    {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(nextScene);
    }

}
