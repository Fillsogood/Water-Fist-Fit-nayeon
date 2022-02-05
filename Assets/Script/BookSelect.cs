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

    public InputField bookNameIF;
    public Text title1;
    public Text contents1;
    public Text translators1;
    public Text author1;
    public Text publisher1;

    public GameObject Java_p1;
    public GameObject Java_p2;
    public GameObject BookPrefab;
    //캠 스폰 포인트
    public Transform spawnPoint;
    //스크롤 뷰 content
    public RectTransform content;
    //비디오 캠 포지션 간격
    private float spaceBetween = 300f;
    public static string url = "http://localhost:59755/WSUforestService.svc/";
    //유저 캠 리스트
    private List<GameObject> BookList;

    void Start()
    {
        BookList = new List<GameObject>(); 
    }
    void Update()
    {
        
    }

    public void Btn_Book()
    {
        Unity_BookSelect();
        boooo();
    }
    public void Btn_BB()
    {
        Bookinfo_P.SetActive(true);
    }


   public void Unity_BookSelect()
   {
        title = bookNameIF.text;
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
   public void Unity_BookSelect1()
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

   private void boooo()
   {

        float spawnY = BookList.Count * spaceBetween;
        Vector3 spawnPosition = new Vector3(0, -spawnY, 0);
        GameObject Book = Instantiate(BookPrefab, spawnPosition, spawnPoint.rotation);

        Book.transform.SetParent(spawnPoint, false);
        content.sizeDelta = new Vector2(0, BookList.Count * spaceBetween + 140);

        UpdatePostions();
   }
   private void UpdatePostions()
    {
        for (int i = 0; i < BookList.Count; i++)
        {
            BookList[i].GetComponent<RectTransform>().anchoredPosition = Vector2.down * spaceBetween * i;
        }
    }

}