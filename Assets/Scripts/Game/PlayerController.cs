using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public enum Controls {Letters, Arrows}

public class PlayerController : MonoBehaviour
{
    private enum bird
    {
        seagull, pelican
    };

    private Rigidbody2D rb;
    private float lastFlapTime = 0f;

    [SerializeField] private Controls playerControls;
    [SerializeField] private bird type;
    [SerializeField] private float flapPower = 500f;
    [SerializeField] private float flapDelay = 0.2f;
    [SerializeField] private float moveSpeed = 5f;


    [SerializeField] private Ball ball;
    [SerializeField] private float hitRange = 1.5f;

    [SerializeField] AudioManager audioManager;

    private Camera cam;
    private float halfWidth;
    private float halfHeight;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;

    }

    // Update is called once per frame
    void Update()
    {
        if(playerControls == Controls.Arrows)
        {
            ArrowInput();
        }
        if (playerControls == Controls.Letters)
        {
            LetterInput();
        }

        
        ResetXVelocityIfNoInput();
        ClampPosition();
    }

    private void LetterInput()
    {
        //Up
        if(Input.GetKeyDown(KeyCode.W))
        {
            Flap();
        }
        //Left
        if (Input.GetKey(KeyCode.A))
        {
            GoLeft();
        }
        //Right
        if (Input.GetKey(KeyCode.D))
        {
            GoRight();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            TryHitBall();
        }
    }

    private void ArrowInput()
    {
        //Up
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Flap();
        }
        //Left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            GoLeft();
        }
        //Right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            GoRight();
        }
        if (Input.GetKey(KeyCode.RightControl))
        {
            TryHitBall();
        }
    }
    private void ResetXVelocityIfNoInput()
    {
        bool pressingLeft = playerControls == Controls.Letters
            ? Input.GetKey(KeyCode.A)
            : Input.GetKey(KeyCode.LeftArrow);

        bool pressingRight = playerControls == Controls.Letters
            ? Input.GetKey(KeyCode.D)
            : Input.GetKey(KeyCode.RightArrow);

        if (!pressingLeft && !pressingRight)
        {
            Vector2 vel = rb.velocity;
            vel.x = 0;
            rb.velocity = vel;
        }
    }

    private void Flap()
    {
        if (Time.time - lastFlapTime < flapDelay) return;

        Vector2 vel = rb.velocity;
        vel.y = flapPower;
        rb.velocity = vel;

        lastFlapTime = Time.time;
    }


    private void TryHitBall()
    {
        if (ball == null) return;

        float distance = Vector2.Distance(transform.position, ball.transform.position);
        if (distance <= hitRange)
        {
            Rigidbody2D ballrb = ball.GetComponent<Rigidbody2D>();
            ballrb.velocity = new Vector2(-ballrb.velocity.x + rb.velocity.x, ballrb.velocity.y + rb.velocity.y);

            if (type == bird.seagull) audioManager.PlaySeagullKick();
            else audioManager.PlayPelicanKick();

            Debug.Log("HIT");
        }
    }



    private void GoLeft()
    {
        Vector2 vel = rb.velocity;
        vel.x = -moveSpeed;
        rb.velocity = vel;
    }

    private void GoRight()
    {
        Vector2 vel = rb.velocity;
        vel.x = moveSpeed;
        rb.velocity = vel;
    }


    private void ClampPosition()
    {
        Vector3 pos = transform.position;

        float minY = cam.transform.position.y - halfHeight;
        float maxY = cam.transform.position.y + halfHeight;

        float screenMidX = cam.transform.position.x;
        float screenLeft = screenMidX - halfWidth;
        float screenRight = screenMidX + halfWidth;

        if (playerControls == Controls.Letters)
        {
            // Left player
            pos.x = Mathf.Clamp(pos.x, screenLeft, screenMidX);
        }
        else if (playerControls == Controls.Arrows)
        {
            // Right player
            pos.x = Mathf.Clamp(pos.x, screenMidX, screenRight);
        }

        pos.y = Mathf.Max(pos.y, minY);

        transform.position = pos;
    }


}
