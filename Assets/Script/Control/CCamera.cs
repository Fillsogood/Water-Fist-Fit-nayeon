using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//캐릭터 카메라
public class CCamera : MonoBehaviour
{
    
    public Transform target;
    public float targetY;

    //회전속도
    public float rotSpeed;
    //줌 인/아웃 속도
    private float scrollSpeed = 100;

    //거리
    public float distance;
    public float minDistance;
    public float maxDistance;

    //Y축 회전
    private float yRot;
    //타겟위치
    private Vector3 targetPos;
    //회전값
    private Vector3 dir;

    void Update()
    {
        mouseControl();
    }

    void LateUpdate()
    {
        lookPlayer();
    }

    //카메라 컨트롤 
    void mouseControl()
    {
        yRot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        distance += -Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;

        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        targetPos = target.position + Vector3.up * targetY;

        dir = Quaternion.Euler(0f, yRot, 0f) * Vector3.forward;
        gameObject.transform.rotation = new Quaternion(30f, 0f, 0f, 0f);
    }

    //카메라 이동 
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

