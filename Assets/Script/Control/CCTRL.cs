using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

//가보자고
//캐릭터 컨트롤
public class CCTRL : MonoBehaviour
{
    //포톤 동기화 
    private PhotonView pv;
    public static CCTRL instance;
    public GameObject MainCamera;
    public GameObject player;

    //player sit
    private bool check_Sit = false;
    //마우스 체크
    private bool check_Mouse = true;
    //player rigidbody
    Rigidbody rg_Player;
    //앉기 이전 값
    Vector3 priviousPosition;

    //클릭한 오브젝트 값
    GameObject target = null;
    //의자 배열
    GameObject[] chairs;
    //의자 객체
    static GameObject chair;

    private Transform tr;
    public float moveSpeed = 5.0f;
    public float turnSpeed = 1.5f;
    private Animator anim;

    public Transform Panel_EBook;
    public Transform Panel_Lobby;

    void Awake()
    {
        pv = GetComponent<PhotonView>();
        rg_Player = player.GetComponent<Rigidbody>();
    }

    void Start()
    {
        chairs = GameObject.FindGameObjectsWithTag("Chair1");
        Panel_EBook = GameObject.Find("EBook").transform.Find("EbookPanel");
        Panel_Lobby =  GameObject.Find("Lobby").transform.Find("LobbyPanel");

        tr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        
        if(!pv.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(MainCamera);
        }
    }

    void Update()
    {
        if (pv.IsMine)
        {
            if (check_Sit == false)
            {
                CharacterMove();
            }

            if (Input.GetMouseButtonDown(0) && (GetClickedObject().tag == "Chair1") && check_Mouse == true)
            {
                priviousPosition = player.transform.position ;
                SitDown();
                anim.SetBool("isSit", true);
                rg_Player.constraints = RigidbodyConstraints.FreezeAll;

            }
            else if (Input.GetMouseButtonDown(1) && check_Sit == true && check_Mouse == false)
            {
                standUp();
                anim.SetBool("isSit", false);
            }
            //NPC콜라이더에 태그 달아주기
            if (Input.GetMouseButtonDown(0) &&  (GetClickedObject().tag == "NPC_EBook"))
            {
                Panel_EBook.gameObject.SetActive(true);
            }
            else if (Input.GetMouseButtonDown(0) &&  (GetClickedObject().tag == "NPC_Lobby"))
            {

                Panel_Lobby.gameObject.SetActive(true);
            }
        }

        else if(!pv.IsMine)
            return;
    }

    //캐릭터 이동
    void CharacterMove()
    {
        float h = Input.GetAxis("SideMove");    //qe
        float v = Input.GetAxis("Vertical");    //ws
        float r = Input.GetAxis("Horizontal");  //ab

        if (Mathf.Approximately(h, 0) && Mathf.Approximately(v, 0))
        {
            anim.SetBool("isWalk", false);
        }
        else
        {
            anim.SetBool("isWalk", true);
            
        }
        anim.SetFloat("xDir", h);
        anim.SetFloat("yDir", v);

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);
        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * r);
        if(Mathf.Approximately(r, 0))
        {
             
                   
        }
        else
        {
            
            if(Mathf.Approximately(h, 1) && Mathf.Approximately(v, 1))
            {
                anim.SetBool("isWalk", true);
                
            }
            
                 
                 
            
            
            
        }
    }

    //레이캐스팅을 사용해 의자 객체 가져오기
    private GameObject GetClickedObject()
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);      
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))     
        {
            target = hit.collider.gameObject;
        }
        return target;
    }
    
    //앉기 로직
    private  void SitDown()
    {
        check_Sit = true;
        check_Mouse = false;
        if (pv.IsMine && check_Sit == true)
        {
            //의자 객체 가져와서 포지션 변경(의자에 맞추어서)
            chair = GetClickedObject();
            player.transform.position = new Vector3(chair.transform.position.x,
                                                    chair.transform.position.y - 1.8f,
                                                    chair.transform.position.z);
            //방향을 변경한다.
            player.transform.rotation = chair.transform.rotation;

            chair.SetActive(false);
        }
    }

    //일어서기 로직
    private void standUp()
    {
        check_Sit = false;
        check_Mouse = true;
        if (pv.IsMine && check_Sit == false)
        {
            player.transform.position = new Vector3(priviousPosition.x,
                                                    priviousPosition.y,
                                                    priviousPosition.z);

            chair.SetActive(true);
        }
    }
}