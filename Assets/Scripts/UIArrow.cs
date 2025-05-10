using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIArrow : MonoBehaviour
{
    public Image arrow;
    public Ball ball;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        arrow.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ball == null)
        {
            return;
        }

        Vector3 ballPos = ball.transform.position;
        float topOfScreenY = cam.transform.position.y + cam.orthographicSize;

        if (ballPos.y > topOfScreenY)
        {
            arrow.enabled = true;

            Vector3 arrowWorldPos = new Vector3(ballPos.x, topOfScreenY - 0.5f, 0f);
            Vector3 arrowScreenPos = cam.WorldToScreenPoint(arrowWorldPos);
            arrow.transform.position = arrowScreenPos;
        }
        else
        {
            arrow.enabled = false;
        }
    }
}
