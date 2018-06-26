using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {
    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 60.0f;

    public Transform lookAt;
    private Transform camTransform;

    private Camera cam;

    //카메라와 플레이어와의 거리
    public float distance = 10.0f;
    //카메라 회전 속도
    public float xSpeed = 220.0f;
    public float ySpeed = 100.0f;

    private float currentX = 0.0f;
    private float currentY = 50.0f;
    private float sensivityX = 4.0f;
    private float sensivityY = 1.0f;

    private void Start()
    {
        camTransform = transform;
        cam = Camera.main;
    }

    private void Update()
    {
        //마우스 스크롤과의 거리계산
        distance -= 1 * Input.mouseScrollDelta.y;

        //마우스 스크롤했을경우 카메라 거리의 Min과Max
        if (distance < 3)
        {
            distance = 3;

        }

        if (distance >= 15)
        {
            distance = 15;
        }

        if (Input.GetMouseButton(1))
        {
            currentX += Input.GetAxis("Mouse X") * xSpeed * 0.015f;
            currentY -= Input.GetAxis("Mouse Y") * ySpeed * 0.015f;

            currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        }
    }

    private void LateUpdate()
    {
        if (!lookAt)
            return;
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);
    }
}
