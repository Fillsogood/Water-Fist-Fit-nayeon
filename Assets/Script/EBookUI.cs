using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBookUI : MonoBehaviour
{

    public GameObject EbookPanel;

    public GameObject InfoPanel;
    //EBook 나가기 메서드
    public void ExitBtn()
    {
        EbookPanel.SetActive(false);
    }
    public void InfoBtn()
    {
        InfoPanel.SetActive(true);
    }
    public void InfoExit()
    {
        InfoPanel.SetActive(false);
    }
}
