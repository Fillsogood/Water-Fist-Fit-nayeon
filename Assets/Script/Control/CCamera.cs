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

    public float CameraMaxDistance = 5.0f;

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

        dir = Quaternion.Euler(0, yRot, 0) * Vector3.forward;
        gameObject.transform.rotation = Quaternion.Euler(30, 0, 0);
    }
    Vector3 _delta = new Vector3(0.0f, 6.0f, -5.0f);
    //카메라 이동 
    void lookPlayer()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, dir, Color.red);
        Physics.Raycast(transform.position, dir, out hit, 1.5f, LayerMask.GetMask("Wall"));

        if(hit.point != Vector3.zero)
        {
            float dist = (hit.point - transform.position).magnitude * 0.8f;
            transform.position = transform.position + dir.normalized * dist;    //근접한 위치로 수정
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);                //방향값 수정

            //transform.Translate(dir * -1 * 3f);
        }
        else
        {
            transform.position = targetPos + dir * -distance;
            transform.LookAt(targetPos);
        }
    }
}

