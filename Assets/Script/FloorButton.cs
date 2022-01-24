using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FloorButton : MonoBehaviour
{
    public GameObject YButton;
    public GameObject NButton;
    public GameObject FloorUI;

    private GameObject Player;

    public Transform FirstFloor;
    public Transform SecondFloor;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }
    #region 계단 올라가기,내려가기,창닫기 버튼 메서드
    public void Moveto1F()
    {
        Player.transform.position = FirstFloor.position;
        FloorUI.SetActive(false);
    }

    public void Moveto2F()
    {
        Player.transform.position = SecondFloor.position;
        FloorUI.SetActive(false);
    }

    public void CancleMove()
    {

        FloorUI.SetActive(false);
    }
    #endregion
}
