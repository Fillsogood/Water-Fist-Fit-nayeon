using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//캐릭터 카메라
public class CCamera : MonoBehaviour
{
    
    public Transform target;
    public float targetY;

    public float xRotMax;
    //
    public float rotSpeed;
    //
    private float scrollSpeed = 100;

    public float distance;
    public float minDistance;
    public float maxDistance;

    private float xRot;
    private float yRot;
    private Vector3 targetPos;
    private Vector3 dir;

    void Update()
    {
        mouseControl();
    }

    void LateUpdate()
    {
        lookPlayer();
    }

    //
    void mouseControl()
    {
        //
        xRot += Input.GetAxis("Mouse Y") * rotSpeed * Time.deltaTime;
        //
        yRot += Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;
        //
        distance += -Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;

        xRot = Mathf.Clamp(xRot, -xRotMax, xRotMax);
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        targetPos = target.position + Vector3.up * targetY;

        dir = Quaternion.Euler(-xRot, yRot, 0f) * Vector3.forward;
    }

    //
    void lookPlayer()
    {
        Debug.DrawRay(transform.position, dir, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, dir.magnitude, LayerMask.GetMask("Wall")))
        {
            float dist = (hit.point - transform.position).magnitude * 0.8f;
            transform.position = transform.position + dir.normalized * dist;
        }
        else
        {
            transform.position = targetPos + dir * -distance;
            transform.LookAt(targetPos);
        }
    }
}

