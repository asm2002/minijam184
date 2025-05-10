using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public GameManager gameManager;

    private Rigidbody2D rb;
    private float startStrength = 5f;

    [SerializeField] private float screenPadding = 0.5f;
    private Camera cam;
    private float ballHalfWidth;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;

        ballHalfWidth = GetComponent<CircleCollider2D>().radius * transform.localScale.x;

        rb.AddForce(Random.value < 0.5f ? Vector2.right * startStrength : Vector2.left * startStrength);
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 ballPosition = transform.position;
        Vector3 screenLeft = cam.ViewportToWorldPoint(Vector3.zero);
        Vector3 screenRight = cam.ViewportToWorldPoint(Vector3.right);

        if (ballPosition.x - ballHalfWidth < screenLeft.x + screenPadding ||
            ballPosition.x + ballHalfWidth > screenRight.x - screenPadding)
        {
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            gameManager.EndGame();
            Debug.Log("Game Over");
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Ignoring collision with player");
        }
    }


}
