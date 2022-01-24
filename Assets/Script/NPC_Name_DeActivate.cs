using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//사서 TMP 트리거 UI 
public class NPC_Name_DeActivate : MonoBehaviour
{
    public GameObject MeetingRoom;
    public GameObject Library;

    public GameObject Activate;
    public GameObject DeActivate;

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
