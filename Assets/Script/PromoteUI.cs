using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromoteUI : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Genre;
    public GameObject BookPanel;

    public GameObject target;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = GetClickedObject();

            if (target.Equals(gameObject))
            {
                Panel.SetActive(true);
            }
        }
    }
    //마우스 클릭하여 객체 얻기
    private GameObject GetClickedObject()
    {
        RaycastHit hit;
        GameObject target = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);      //마우스 포인트 근처 좌표를 만든다.
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))     //마우스 근처에 오브젝트가 있는지 확인
        {
            target = hit.collider.gameObject;
        }
        return target;
    }

    private void OnTriggerEnter(Collider other)
    {
        Genre.SetActive(true);
        target.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Genre.SetActive(false);
        target.SetActive(false);
    }

    public void BookBtn()
    {
        BookPanel.SetActive(true);
    }

    public void ExitBtn()
    {
        Panel.SetActive(false);
    }
}
