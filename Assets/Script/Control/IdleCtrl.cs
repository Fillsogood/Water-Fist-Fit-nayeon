using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCtrl : MonoBehaviour
{
    public GameObject player;
    private static Animator anim;

    public CCTRL cctrl;

    void Start()
    {
        anim = player.GetComponent<Animator>();
    }


    void Update()
    {
        if(cctrl.enabled == false)
        {
            StartCoroutine(CheckAnimationState());
        }
    }

    IEnumerator CheckAnimationState()
    {

	    while (!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) 
	    { 
		    cctrl.enabled = true;
		    yield return null;
	    }

    }
}
