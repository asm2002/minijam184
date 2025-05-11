using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    private float startStrength = 3f;

    [SerializeField] AudioManager audioManager;

    public int rallyLength;
    public int lastHitPlayer;
    public float ballHeight;
    public float highest;
    public float score;

    public bool gameOver = false;




    // Start is called before the first frame update
    void Start()
    {
        ballHeight = transform.position.y;
        highest = ballHeight;
        rallyLength = 0;
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(Random.value < 0.5f ? Vector2.right * startStrength : Vector2.left * startStrength);
    }


    // Update is called once per frame
    void Update()
    {
        ballHeight = transform.position.y;
        if(ballHeight > highest)
        {
            highest = ballHeight;
        }
        if(ballHeight <= -3.2 && gameOver == false)
        {
            gameOver = true;
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        Debug.Log("rally:" + rallyLength);
        Debug.Log("highest score" + highest);
        score = (rallyLength * 100) + (highest * 10);
        Debug.Log("score:" + score);
        GameManager.Instance.EndGame();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            gameOver = true;
            GameOver();
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            if(lastHitPlayer == 2 || rallyLength == 0)
            {
                rallyLength++;
            }

            lastHitPlayer = 1;
            audioManager.PlaySeagullKick();
        }
        else if (collision.gameObject.CompareTag("Player2"))
        {
            if (lastHitPlayer == 1 || rallyLength == 0)
            {
                rallyLength++;
            }

            lastHitPlayer = 2;
            audioManager.PlayPelicanKick();
        }

        audioManager.PlayBall();

    }


}
