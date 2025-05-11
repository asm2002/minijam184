using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private Rigidbody2D rb;
    private float startStrength = 3f;

    [SerializeField] private float screenPadding = 0.5f;
    private Camera cam;
    private float ballHalfWidth;
    public int rallyLength;
    public int lastHitPlayer;
    public float ballHeight;
    public float highest;
    public float score;




    // Start is called before the first frame update
    void Start()
    {
        ballHeight = transform.position.y;
        highest = ballHeight;
        rallyLength = 0;
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
        ballHeight = transform.position.y;
        if(ballHeight > highest)
        {
            highest = ballHeight;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            GameManager.Instance.EndGame();
            Debug.Log("Game Over");
            Debug.Log("rally:" + rallyLength);
            Debug.Log("highest score" + highest);
            score = (rallyLength * 100) + (highest * 10);
            Debug.Log("score:" + score);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            if(lastHitPlayer == 2)
            {
                rallyLength++;
            }

            lastHitPlayer = 1;
        }
        else if (collision.gameObject.CompareTag("Player2"))
        {
            if (lastHitPlayer == 1)
            {
                rallyLength++;
            }

            lastHitPlayer = 2;
        }
    }


}
