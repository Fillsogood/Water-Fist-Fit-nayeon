using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class FloorUI : MonoBehaviour
{
    public GameObject FloorPanel;
    public PhotonView pv;
    //사용자한테 계단 UI 보이게 하는 트리거
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"&&pv.IsMine)
        {
            FloorPanel.SetActive(true);
        }
    }
}
