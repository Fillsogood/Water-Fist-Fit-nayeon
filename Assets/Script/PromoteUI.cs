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
    //���콺 Ŭ���Ͽ� ��ü ���
    private GameObject GetClickedObject()
    {
        RaycastHit hit;
        GameObject target = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);      //���콺 ����Ʈ ��ó ��ǥ�� �����.
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))     //���콺 ��ó�� ������Ʈ�� �ִ��� Ȯ��
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
