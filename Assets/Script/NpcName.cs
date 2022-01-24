using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCName : MonoBehaviour
{
    public GameObject MeetingRoom;
    public GameObject Library;

    public GameObject Activate;
    public GameObject DeActivate;
    //NPC이름 활성화 트리거
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            MeetingRoom.SetActive(true);
            Library.SetActive(true);
            Activate.SetActive(false);
            DeActivate.SetActive(true);
        }       
    }
}
