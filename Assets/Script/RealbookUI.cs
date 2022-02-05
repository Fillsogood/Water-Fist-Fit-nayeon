using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RealbookUI : MonoBehaviour
{
    public static string url = "http://localhost:59755/WSUforestService.svc/";
         
    //WCF_ID
    private static int bookID;
    //WCF_type
    private static string type;
    //WCF_title 
    private static string title;
    //WCF_Contents
    private static string contents;
    //WCF_isbn
    private static string isbn;
    //WCF_author
    private static string author;
    //WCF_Publisher
    private static string publisher;
    //WCF_translators
    private static string translators;
    //WCF_thumbnail
    private static string thumnail;
    //WCF_status
    private static string status;
    //WCF_Bestseller
    private static int bestSeller;

    //실물책 입력 
    public InputField inputField;
    //실물책 타입
    string realType = "real";

    //Unity_title
    public Text t_Title;
    //Unity_Contents
    public Text t_Contents;
    //Unity_Author
    public Text t_Author;
    //Unity_Publisher
    public Text t_Publisher;
    //Unity_Thumnail
    public RawImage ri_Thumnail;

    //EBook전체 패널
    public GameObject EbookPanel;
    //검색도서정보 패널
    public GameObject infoPanel;
    

    #region 검색 이벤트
    
    public void e_realBookSearch()
    {
        //두개의 엔터키 입력
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
        {
            b_realBookSearch();
        }
    }

    //디비 정보를 가져오고 오브젝트에 초기화
    public void b_realBookSearch()
    {
        //info패널 활성화
        InfoBtn();

        if (StringAvailable(inputField.text))
        {
            //디비 정보 가져오기 
            GetBookList(inputField.text, realType);

            //가져온 디비 정보를 통해서, 텍스트 채우기
            t_Title.text = title;
            t_Contents.text = contents;
            t_Author.text = author;
            t_Publisher.text = publisher;

            StartCoroutine(GetTexture(ri_Thumnail));
        }

        this.inputField.text = "";
    }

    IEnumerator GetTexture(RawImage img)
    {
        var url = thumnail;
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            img.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
    }

    #endregion

    #region 그 외 메서드

    //WCF로 실물책 데이터 가져오기
    private void GetBookList(string title, string type)
    {
        string sendurl = url + "Unity_BookSelect";

        HttpWebRequest httpWebRequest = WebRequest.Create(new Uri(sendurl)) as HttpWebRequest;
        httpWebRequest.Method = "POST";
        httpWebRequest.ContentType = "application/json; charset=utf-8";

        string msg = "{\"title\":\"" + title + "\",\"type\":\"" + type + "\"}";

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
            string[] bookInfo = result2[1].Split('@');

            bookID = int.Parse(bookInfo[0]);
            type = bookInfo[1];
            title = bookInfo[2];
            contents = bookInfo[3];
            isbn = bookInfo[4];
            author = bookInfo[5];
            publisher = bookInfo[6];
            translators = bookInfo[7];
            thumnail = bookInfo[8];
            status = bookInfo[9];
            bestSeller = int.Parse(bookInfo[10]);

            //thumnail 재정리
            string[] s_thumnail = thumnail.Split('\\');
            thumnail = s_thumnail[0] + s_thumnail[1] + s_thumnail[2] + s_thumnail[3]
                + s_thumnail[4] + s_thumnail[5] + s_thumnail[6];
            //https:\/\/library.wsu.ac.kr\/Sponge\/Images\/bookDefaults\/MMbookdefaultsmall.png
        }
        catch (WebException e)
        {
            Debug.Log(e.Message);
        }
    }

    //inputField가 비어있는 지 확인
    bool StringAvailable(string inputField가)
    {
        if (string.IsNullOrWhiteSpace(inputField가)) return false;
        return true;
    }

    //패널 비/활성화 메서드
    public void ExitBtn()
    {
        EbookPanel.SetActive(false);
    }
    public void InfoBtn()
    {
        infoPanel.SetActive(true);
    }
    public void InfoExit()
    {
        infoPanel.SetActive(false);
    }

    #endregion
}
