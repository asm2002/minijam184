using DG.Tweening;
using UnityEngine;

public class UIWobble : MonoBehaviour
{

    [SerializeField] float animationTime = 5;
    [SerializeField] float rotationAmount = 5;
    [SerializeField] float scaleAmount = 1.1f;

    RectTransform rect;

    Sequence rotationSequence;
    Sequence scaleSequence;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();

        rotationSequence = DOTween.Sequence().SetUpdate(true);
        rotationSequence.Append(rect.DORotate(new Vector3(0, 0, rotationAmount), animationTime).SetEase(Ease.InOutQuad));
        rotationSequence.Append(rect.DORotate(new Vector3(0, 0, -rotationAmount), animationTime).SetEase(Ease.InOutQuad));
        rotationSequence.SetLoops(-1, LoopType.Yoyo);

        scaleSequence = DOTween.Sequence().SetUpdate(true);
        scaleSequence.Append(rect.DOScale(new Vector3(scaleAmount, scaleAmount, 1), animationTime / 2).SetEase(Ease.InOutQuad));
        scaleSequence.Append(rect.DOScale(new Vector3(1, 1, 1), animationTime / 2).SetEase(Ease.InOutQuad));
        scaleSequence.SetLoops(-1, LoopType.Yoyo);

        rotationSequence.Play();
        scaleSequence.Play();
    }

}
