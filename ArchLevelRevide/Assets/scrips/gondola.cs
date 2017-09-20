using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gondola : MonoBehaviour {


    public Transform target;
    public float speed;

    public bool goAllwaysBack;

    private Vector3 startPos;

    private GameObject targetPassanger;
    private float distToStart;
    private float distToTarget;

    public GameObject soundEmitter;

    private bool isFinished = false;

    void Start()
	{
        this.startPos = this.gameObject.transform.position;
	}

    void Update() {
        if (this.isFinished)
            return;

        this.distToStart = Vector3.Distance(this.gameObject.transform.position, this.startPos);
        this.distToTarget = Vector3.Distance(this.gameObject.transform.position, this.target.position);

        if (this.targetPassanger != null && this.distToTarget > 0.1f)
        {
            Vector3 dir = this.target.position - this.gameObject.transform.position;
            dir = dir.normalized * this.speed * Time.deltaTime;

            this.gameObject.transform.position += dir;
            this.targetPassanger.transform.position += dir;
            if (this.soundEmitter != null)
                this.soundEmitter.SetActive(true);
            //transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }

        else if (this.targetPassanger != null && this.distToTarget < 0.1f)
        {
            if (this.soundEmitter != null)
                this.soundEmitter.SetActive(false);
            if (!this.goAllwaysBack)
                this.isFinished = true;
        }

        else if (this.targetPassanger == null && this.distToStart > 0.1f)
        {
            Vector3 dir = this.startPos - this.gameObject.transform.position;
            dir = dir.normalized * (this.speed * 2) * Time.deltaTime;

            if (this.soundEmitter != null)
                this.soundEmitter.SetActive(true);

            this.gameObject.transform.position += dir;
        }

        else
        {
            if (this.soundEmitter != null)
                this.soundEmitter.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        this.targetPassanger = other.gameObject;
    }
    
    void OnTriggerExit(Collider other)
    {
        this.targetPassanger = null;
    }
}