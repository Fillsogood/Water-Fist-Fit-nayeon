using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Name_DeActivate : MonoBehaviour
{
    public GameObject MeetingRoom;
    public GameObject Library;

    public GameObject Activate;
    public GameObject DeActivate;
    //NPC 이름 비활성화 트리거
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            MeetingRoom.SetActive(false);
            Library.SetActive(false);
            Activate.SetActive(true);
            DeActivate.SetActive(false);
        }
    }
}
