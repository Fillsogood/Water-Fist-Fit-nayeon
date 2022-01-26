using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PromoteUI : MonoBehaviour
{
    public GameObject Panel;
    public GameObject BookPanel;
    private int randomNumber;

    private void Start()
    {
        randomNumber = SpawnManager.myCheck;
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(randomNumber == SpawnManager.myCheck)
        {
            Panel.SetActive(true);
            Debug.Log(randomNumber);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Panel.SetActive(false);
        
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
