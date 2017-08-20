using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeCrashController : MonoBehaviour {

    private bool started = false;
    public Animator treeAnimator;
    public GameObject soundEmitter;

    void Start()
    {
        this.treeAnimator = this.gameObject.GetComponent(typeof(Animator)) as Animator;
        //Debug.Log(this.treeAnimator);
    }

    void Update()
    {
        //Debug.Log(this.treeAnimator);
    }


    public void ActivateCrash(Animator ani)
    {
        if (!this.started)
        {
            if (this.soundEmitter != null)
                this.soundEmitter.SetActive(true);
            this.started = true;
            ani.SetTrigger("start");
        }
    }
}
