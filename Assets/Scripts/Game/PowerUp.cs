using DG.Tweening;
using UnityEngine;

public class TimePowerup : MonoBehaviour
{
    public float timeToAdd;
    public float visibleLifespan = 10f;

    private SpriteRenderer sr;
    private bool isFading = false;
    private Camera cam;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        cam = Camera.main;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Vector3 viewportPos = cam.WorldToViewportPoint(transform.position);
            bool fullyOnScreen =
                viewportPos.x >= 0f && viewportPos.x <= 1f &&
                viewportPos.y >= 0f && viewportPos.y <= 1f;

            if (!fullyOnScreen)
            {
                // Do nothing if it's not on screen
                Debug.Log("Powerup hit offscreen — ignoring.");
                return;
            }
            Timer.Instance.AddTime(timeToAdd);

            sr.DOKill();

            Destroy(gameObject);

            Debug.Log("Ball hit on powerup");
        }
    }
    void Update()
    {
        if (isFading) return;

        Vector3 viewportPos = cam.WorldToViewportPoint(transform.position);
        bool fullyOnScreen =
            viewportPos.x >= -0.1f && viewportPos.x <= 1.1f &&
            viewportPos.y >= -0.1f && viewportPos.y <= 1.1f;

        if (fullyOnScreen)
        {
            isFading = true;
            sr.DOColor(new Color(sr.color.r, sr.color.g, sr.color.b, 0f), visibleLifespan);
            Destroy(gameObject, visibleLifespan);
            Debug.Log("Powerup fully on screen, starting fade.");
        }
    }
}
