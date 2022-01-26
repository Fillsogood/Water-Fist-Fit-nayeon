using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromoteUI : MonoBehaviour
{
    public GameObject Panel;
    public GameObject BookPanel;


    private void OnTriggerEnter(Collider other)
    {
        Panel.SetActive(true);
       
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
