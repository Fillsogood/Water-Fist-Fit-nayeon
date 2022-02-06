using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine.UI;
using TMPro;
public class BookData : MonoBehaviour
{
   
     private static string _title;
     private static string _author;

     private Text bookInfoText;
    static int Count;
     
    public static string url = "http://localhost:59755/WSUforestService.svc/";

    void Awake()
    {
        bookInfoText = GetComponentInChildren<Text>();
        Unity_BookwishlistCount();
        Unity_BookCheckwishlist();
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
        
        try
        {
            using (HttpWebResponse response = httpWebRequest.GetResponse() as HttpWebResponse)
                result = new StreamReader(response.GetResponseStream()).ReadToEnd().ToString();
            Debug.Log(result);

            //["Java의정석@남궁성","Do it! 점프투파이썬@박응용"]
            
            string[] result2 = result.Split('"');
            string[] bookInfo = result2[1].Split('@');
            Debug.Log(Count);
            for(int i = 0;i<Count;i++)
            {
                if(i%2==1)
                {
                    bookInfoText.text = bookInfo[i]+",";
                    Debug.Log(bookInfoText.text);
                }
                if(i%2==0)
                {
                     bookInfoText.text = bookInfo[i]+"\n";
                     Debug.Log(bookInfoText.text);
                }
            }
            Debug.Log(bookInfoText.text);
            
        }
        catch(WebException e)
        {
            Debug.Log(e.Message);
        } 
   } 


    public void Unity_BookwishlistCount()
   {
        string sendurl = url + "Unity_BookwishlistCount"; 

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
        
        try
        {
            using (HttpWebResponse response = httpWebRequest.GetResponse() as HttpWebResponse)
                result = new StreamReader(response.GetResponseStream()).ReadToEnd().ToString();
            string[] result2 = result.Split('"');
            Count = int.Parse(result2[1]);
            Debug.Log(Count);
        }
        catch(WebException e)
        {
            Debug.Log(e.Message);
        } 
   } 
}
