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

    [SerializeField] private GameObject sandEffectPrefab;
    private ParticleSystem sandEffect;


    private void Start()
    {
        GameObject effectObj = Instantiate(sandEffectPrefab);
        sandEffect = effectObj.GetComponent<ParticleSystem>();
        sandEffect.Stop();
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

    public void startGame()
    {
        GetComponent<Rigidbody2D>().simulated = true;

        ballHeight = transform.position.y;
        highest = ballHeight;
        rallyLength = 0;
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(Random.value < 0.5f ? Vector2.right * startStrength : Vector2.left * startStrength);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameOver == false)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                gameOver = true;
                GameOver();
            }
            else if (collision.gameObject.CompareTag("Player"))
            {
                // Sand particle spawning
                ContactPoint2D contact = collision.GetContact(0);
                Vector2 hitNormal = contact.normal;

                sandEffect.transform.position = contact.point;
                sandEffect.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, hitNormal));
                sandEffect.Play();

                if (lastHitPlayer == 2 || rallyLength == 0)
                {
                    rallyLength++;
                }

                lastHitPlayer = 1;
            }
            else if (collision.gameObject.CompareTag("Player2"))
            {
                ContactPoint2D contact = collision.GetContact(0);
                Vector2 hitNormal = contact.normal;

                sandEffect.transform.position = contact.point;
                sandEffect.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, hitNormal));
                sandEffect.Play();

                if (lastHitPlayer == 1 || rallyLength == 0)
                {
                    rallyLength++;
                }

                lastHitPlayer = 2;
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
