using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CustomizeUI : MonoBehaviour
{
    public GameObject CPanel;
    public GameObject MPanel;
    public GameObject TPanel;
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
    }
    //나가기 선택
    public void ExitBtnClick()
    {  
        Application.Quit();
    }
    //타이틀 숨기기
    public void TitleBtnClick()
    {
        TPanel.SetActive(false); 
    }
}
