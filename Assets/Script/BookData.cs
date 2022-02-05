using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine.UI;
using Photon.Pun;
public class BookData : MonoBehaviour
{
   
     private static string _title;
     private static string _author;
     private static Text t;
     private static Text a;
     private GameObject b;
    public static string url = "http://localhost:59755/WSUforestService.svc/";
    
    public static string title
    {
        get
        {
            return _title;
        }
        set
        {
            _title = value;

            // 룸 정보 표시
            t.text =_title;
            // 버튼 클릭 이벤트에 함수 연결
            //GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnEnterRoom(_roomInfo.Name));
        }
    }
    public static string author
    {
        get
        {
            return _author;
        }
        set
        {
            _author = value;

            // 룸 정보 표시
            a.text = _author;
            // 버튼 클릭 이벤트에 함수 연결
            //GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnEnterRoom(_roomInfo.Name));
        }
    }

    void Awake()
    {
        b = GameObject.Find("Panel");
        foreach(var text in b.GetComponentsInChildren<Text>(true))
        {
            if(text.name == "BookTitle")
            {
                t = GetComponentInChildren<Text>();
            }
            if(text.name == "Author")
            {
                a = GetComponentInChildren<Text>(); 
            }
        }   
    }
     public void Unity_BookCheckwishlist()
   {
        string sendurl = url + "Unity_BookCheckwishlist"; 

        HttpWebRequest httpWebRequest = WebRequest.Create(new Uri(sendurl)) as HttpWebRequest;
        httpWebRequest.Method = "POST";
        httpWebRequest.ContentType = "application/json; charset=utf-8";

        string msg = "{\"W_id\":\"" + WCF.UserID.ToString() + "\"}";
        Debug.Log(msg);

        byte[] bytes = Encoding.UTF8.GetBytes(msg);
        httpWebRequest.ContentLength = (long)bytes.Length;
        using (Stream requestStream = httpWebRequest.GetRequestStream())
            requestStream.Write(bytes, 0, bytes.Length);

        string result = null;
        
        try{
            using (HttpWebResponse response = httpWebRequest.GetResponse() as HttpWebResponse)
                result = new StreamReader(response.GetResponseStream()).ReadToEnd().ToString();
            Debug.Log(result);

            string[] result2 = result.Split('"');
            string[] bookInfo = result2[1].Split('@');
            t.text = bookInfo[2];
            a.text = bookInfo[5];
        }
        catch(WebException e)
        {
            Debug.Log(e.Message);
        } 
   } 
}
