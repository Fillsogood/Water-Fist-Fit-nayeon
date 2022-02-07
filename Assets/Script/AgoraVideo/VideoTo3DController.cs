using UnityEngine.UI;
using UnityEngine;
using agora_gaming_rtc;
using System.Collections.Generic;
using System;
using agora_utilities;
using Photon.Pun;
using System.Collections;

public class VideoTo3DController : MonoBehaviour
{
    //채널이름
    protected string mChannel;
    //캠기능
    private IRtcEngine mRtcEngine;
    //AppID
    public string AppID = "3ab0f8434af0422fbabfdd0b4e24c506";
    //AppID 초기화 여부
    private bool _initialized = false;
    //나의 아이디
    private uint myuid=0;

    #region 아고라 엔진 및 채널 입장
    //아고라 엔진 활성화
    private void AgoraAtivation()
    {
        //string channelName = PhotonNetwork.CurrentRoom.name;    //채널 나누기
        string channelName = "please";                            //채널이름 초기화  

        //채널이름 체크
        if (string.IsNullOrEmpty(channelName))
        {
            Debug.LogError("Channel name can not be empty!");
            return;
        }
        //AppID 체크 
        if (!_initialized)
        {
            Debug.LogError("AppID null or app is not initialized properly!");
            return;
        }

        // 채널 가입
        Join(channelName);
        
        mRtcEngine.EnableVideo();
        mRtcEngine.EnableVideoObserver();
    }

    //채널 조인
    public void Join(string channel)
    {
        Debug.Log("calling join (channel = " + channel + ")");

        if (mRtcEngine == null)
            return;

        mChannel = channel;

        // set callbacks (optional)
        mRtcEngine.OnJoinChannelSuccess = OnJoinChannelSuccess;
        mRtcEngine.OnUserOffline = OnUserOffline;

        // join channel
        mRtcEngine.JoinChannel(channel, null, 0);

        Debug.Log("initializeEngine done");

    }
    
    //AppId체크 함수
    private void CheckAppId()
    {
        //조건이 false면 메시지를 호출한다.
        Debug.Assert(AppID.Length > 10, "Please fill in your AppId first on Game Controller object.");
        if (AppID.Length > 10)
        {

            _initialized = true; //초기화 ok
        }
    }
    #endregion

    #region 아고라 엔진 및 채널 떠나기

    //채널 떠나기
    private void Leave()
    {
        Debug.Log("calling leave");

        if (mRtcEngine == null)
            return;

        // leave channel
        mRtcEngine.LeaveChannel();
        // 비디오 탐색을 중지
        mRtcEngine.DisableVideoObserver();
    }

    // 아고라 엔진 끄기
    private void UnloadEngine()
    {
        Debug.Log("calling unloadEngine");

        // delete
        if (mRtcEngine != null)
        {
            IRtcEngine.Destroy();  //엔진 Destroy
            mRtcEngine = null;
        }
    }

    #endregion 
    
    #region 콜백
    // implement engine callbacks
    protected virtual void OnJoinChannelSuccess(string channelName, uint uid, int elapsed)
    {
        Debug.Log("JoinChannelSuccessHandler: uid = " + uid);
        myuid = uid;  
        CreateUserVideoSurface(uid,true);
    }
    // 나를 기준으로 유저가 들어온 후에 호출되는 콜백
    protected virtual void OnUserJoined(uint uid, int elapsed)  //elapsed : 지연시간 
    {
        Debug.Log("onUserJoined: uid = " + uid + " elapsed = " + elapsed);  
        CreateUserVideoSurface(uid, false);    
    }

    // 사용자가 나갈때 콜백되는 함수
    protected virtual void OnUserOffline(uint uid, USER_OFFLINE_REASON reason)
    {    
        RemoveUserVideoSurface(uid);   
    }
    #endregion

    #region 콜백에서 사용하는 메서드
    //캠 만들기
    private void CreateUserVideoSurface(uint uid,bool isLocalUser)
    {

        // Attach the SDK Script VideoSurface for video rendering
        GameObject quad = GameObject.Find("Quad");
        if (ReferenceEquals(quad, null))
        {
            Debug.Log("failed to find Quad");
            return;
        }
        else
        {
            quad.AddComponent<VideoSurface>();
        }

        // Update our VideoSurface to reflect new users
        VideoSurface newVideoSurface = quad.GetComponent<VideoSurface>();
        if(newVideoSurface == null)
        {
            Debug.LogError("CreateUserVideoSurface() - VideoSurface component is null on newly joined user");
            return;
        }
         if (isLocalUser == false)
        {
            newVideoSurface.SetForUser(uid);
        }
        newVideoSurface.SetGameFps(30);
    }

    private void RemoveUserVideoSurface(uint deletedUID)
    {
        // Attach the SDK Script VideoSurface for video rendering
        GameObject quad = GameObject.Find("Quad");
        if (ReferenceEquals(quad, null))
        {
            Debug.Log("failed to find Quad");
            return;
        }
        else
        {  
                Destroy(quad.GetComponent<VideoSurface>());  
        }

    }
    #endregion

    public void Btn_Share3DVideo()
    {
        CheckAppId();                               //AppID 확인

        if (mRtcEngine != null)                     //엔진이 있으면 삭제
        {
            IRtcEngine.Destroy();
        }

        mRtcEngine = IRtcEngine.GetEngine(AppID);   //아고라 엔진 불러오기
        AgoraAtivation();                           //아고라 엔진 활성화

        Join("please");
    }

    public void Btn_StopShare3DVideo()
    {
        Leave();
        UnloadEngine();
        RemoveUserVideoSurface(myuid);
    }


}
