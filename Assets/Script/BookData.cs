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
        Unity_BookCheckwishlist();
        Unity_BookwishlistCount();
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

            
            for(int i = 0;i<Count;i++)
            {
                if(i%2==1&&i==0)
                {
                    bookInfoText.text = bookInfo[i]+",";
                }
                else
                {
                     bookInfoText.text = bookInfo[i]+"\n";
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
            Debug.Log(result);
            Count = int.Parse(result);

            
        }
        catch(WebException e)
        {
            Debug.Log(e.Message);
        } 
   } 
}
