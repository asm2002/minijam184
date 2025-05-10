using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Buttons : MonoBehaviour
{

    [Header("Buttons")]
    [SerializeField] float selectTime = 0.25f;
    [SerializeField] Vector2 buttonSize = new Vector2(291.5f, 88.5f);
    [SerializeField] float growthAmount = 1.05f;

    public void ButtonSelected(GameObject button)
    {
        RectTransform transform = button.GetComponent<RectTransform>();
        Image image = button.GetComponent<Image>();
        transform.DOSizeDelta(buttonSize * growthAmount, selectTime).SetUpdate(true);
    }

    public void ButtonDeselected(GameObject button)
    {
        RectTransform transform = button.GetComponent<RectTransform>();
        Image image = button.GetComponent<Image>();
        transform.DOSizeDelta(buttonSize, selectTime).SetUpdate(true);
    }

}
