using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine.UI;
using Photon.Pun;
public class BookSelect : MonoBehaviour
{
    //^A^
    private PhotonView pv;
    //북ID
    private static int bookID;
    //북type
    private static string type;
    //북title 
    private static string title;
    //북Contents
    private static string contents;
    //북isbn
    private static string isbn;
    //북author
    private static string author;
    //북Publisher
    private static string publisher;
    //북translators
    private static string translators;
    //북thumbnail
    private static string thumnail;
    //북status
    private static string status;
    //북Bestseller
    private static int bestSeller;

    //Bookinfo
    public GameObject Bookinfo_P;
    public Text title1;
    public Text contents1;
    public Text translators1;
    public Text author1;
    public Text publisher1;

    public GameObject BookPrefab;
    //캠 스폰 포인트
    public Transform spawnPoint;
    //스크롤 뷰 content
    public RectTransform content;
    //비디오 캠 포지션 간격
    private float spaceBetween = 80f;
    public static string url = "http://localhost:59755/WSUforestService.svc/";
    //유저 캠 리스트
    private List<GameObject> BookList;
    public Text PP;

    void Start()
    {
         BookList = new List<GameObject>();
         PP=GetComponent<Text>();   
    }
    // 책 정보 띄우는 패널
    public void Btn_Book1()
    {
        Unity_BookSelect("Java의정석");
        Bookinfo_P.SetActive(true);
    }
    public void Btn_Book2()
    {
        Unity_BookSelect("혼자공부하는자바");
        Bookinfo_P.SetActive(true);
    }
    //찜 목록 추가
    public void Btn_AddWishList()
    {
        if(title1.text == "Java의정석")
        {
            Unity_AddWish("10");
        }
        else if(title1.text == "혼자공부하는자바")
        {
            Unity_AddWish("4");
        }
    }
    //찜 목록 해제
    public void Btn_RemoveWish()
    {
        if(title1.text == "Java의정석")
        {
            Unity_RemoveWish("10");
        }
        else if(title1.text == "혼자공부하는자바")
        {
            Unity_RemoveWish("4");
        }
    }
    //찜 추가
    public void Unity_AddWish(string b_id)
   {
        string sendurl = url + "Unity_AddWish"; 

        HttpWebRequest httpWebRequest = WebRequest.Create(new Uri(sendurl)) as HttpWebRequest;
        httpWebRequest.Method = "POST";
        httpWebRequest.ContentType = "application/json; charset=utf-8";

        string msg = "{\"W_id\":" + WCF.UserID.ToString() + ",\"b_id\":" + b_id + "}";
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
            PP.text=result;

        }
        catch(WebException e)
        {
            Debug.Log(e.Message);
        } 
   } 


    //찜 해제
    public void Unity_RemoveWish(string b_id)
   {
        string sendurl = url + "Unity_RemoveWish"; 

        HttpWebRequest httpWebRequest = WebRequest.Create(new Uri(sendurl)) as HttpWebRequest;
        httpWebRequest.Method = "POST";
        httpWebRequest.ContentType = "application/json; charset=utf-8";

        string msg = "{\"W_id\":" + WCF.UserID.ToString() + ",\"b_id\":" + b_id + "}";
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
            PP.text=result;

        }
        catch(WebException e)
        {
            Debug.Log(e.Message);
        } 
   } 

    #region 찜 목록
   public void Unity_BookSelect(string title)
   {
        string sendurl = url + "Unity_BookSelect"; 

        HttpWebRequest httpWebRequest = WebRequest.Create(new Uri(sendurl)) as HttpWebRequest;
        httpWebRequest.Method = "POST";
        httpWebRequest.ContentType = "application/json; charset=utf-8";

        string msg = "{\"title\":\"" + title + "\",\"type\":\"" + "e" + "\"}";
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

            //bookID = int.Parse(bookInfo[0]);
            //type = bookInfo[1];
            title1.text = bookInfo[2];
            title = bookInfo[2];
            contents1.text = bookInfo[3];
            //isbn = bookInfo[5];
            author1.text = bookInfo[5];
            publisher1.text = bookInfo[6];
            translators1.text = bookInfo[7];
            //thumnail = bookInfo[9];
            //status = bookInfo[10];
            //bestSeller = int.Parse(bookInfo[11]);

        }
        catch(WebException e)
        {
            Debug.Log(e.Message);
        } 
   } 

  
    #endregion

}