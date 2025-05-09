using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target1;
    public Transform target2;

    public Transform bg1;
    public Transform bg2;

    public float VerticalCameraOffset;
    private float size;

    private Vector3 cameraTargetPos = new Vector3();
    private Vector3 bg1Target = new Vector3();
    private Vector3 bg2Target = new Vector3();


    // Start is called before the first frame update
    void Start()
    {
        size = bg1.GetComponent<BoxCollider2D>().size.y * 10;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPos = SetPos(cameraTargetPos, 0, ((target1.position.y + target2.position.y)/2)
            + VerticalCameraOffset, -10);
        transform.position = Vector3.Lerp(transform.position, targetPos, 0.2f);

        // Go Up
        if (transform.position.y >= bg1.position.y)
        {
            bg1.position = SetPos(bg1Target, bg1.position.x, bg2.position.y + size, 0);
            SwitchBg();
        }


        // Go Down
        if(transform.position.y < bg1.position.y)
        {
            bg2.position = SetPos(bg2Target, 0, bg1.position.y - size, 0);
            SwitchBg();
        }
    }

    private void SwitchBg()
    {
        Transform temp = bg1;
        bg1 = bg2;
        bg2 = temp;
    }

    private Vector3 SetPos(Vector3 pos, float x, float y, float z)
    {
        pos.x = x; pos.y = y; pos.z = z;
        return pos;
    }
}
