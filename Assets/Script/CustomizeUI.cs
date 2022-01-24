using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ŀ���͸����� UI
public class CustomizeUI : MonoBehaviour
{
    public GameObject CPanel;
    public GameObject MPanel;
    public GameObject FPanel;
    //남자 선택
    public void MaleBtnClick()
    {
        CPanel.SetActive(false);
        MPanel.SetActive(true);
    }
    //여자 선택
    public void FemaleBtnClick()
    {
        CPanel.SetActive(false);
        FPanel.SetActive(true);
    }
    //나가기 선택
    public void ExitBtnClick()
    {
        CPanel.SetActive(false);
        Application.Quit();
    }
}
