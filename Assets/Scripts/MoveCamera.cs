using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour
{

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
        if (Input.GetButton("Fire2"))
        {
            Debug.Log("Right Mouse Button Pressed");
            Camera.main.gameObject.GetComponent<GazeDetection>().CheckGaze();
        }
    }
}