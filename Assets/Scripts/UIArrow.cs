using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIArrow : MonoBehaviour
{
    public GameObject arrow;
    public Image ballSprite;
    public Ball ball;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        arrow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ball == null)
            return;

        Vector3 ballPos = ball.transform.position;
        float topOfScreenY = cam.transform.position.y + cam.orthographicSize;

        bool isAboveScreen = ballPos.y > topOfScreenY;

        arrow.SetActive(isAboveScreen);

        if (isAboveScreen)
        {
            float zRotation = ball.transform.eulerAngles.z;
            ballSprite.transform.rotation = Quaternion.Euler(0f, 0f, zRotation);

            Vector3 arrowWorldPos = new Vector3(ballPos.x, topOfScreenY - 1.2f, ballPos.z); 
            Vector3 arrowScreenPos = cam.WorldToScreenPoint(arrowWorldPos);

            arrow.transform.position = arrowScreenPos;
        }
    }


}
