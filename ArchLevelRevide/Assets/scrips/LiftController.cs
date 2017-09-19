using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftController : MonoBehaviour {
    
    public float speed;

    public Vector3 bPosOffset;
    private Vector3 aPos;
    private Vector3 bPos;
    private Vector3 lastPos;
    private Vector3 nextPos;
    public bool aFallResetPos;
    public bool bFallResetPos;
    private float maxDistance;
    public AnimationCurve speedAnimationCurve;

    private GameObject targetPassanger;
    private float distToStart;
    private float distToTarget;

    public GameObject soundEmitter;

    public LiftController()
    {
        this.aFallResetPos = true;
        this.bFallResetPos = false;
    }

    void Start()
    {
        this.aPos = this.gameObject.transform.position;
        this.bPos = this.aPos + this.bPosOffset;
        this.maxDistance = Vector3.Distance(this.aPos, this.bPos);
        this.nextPos = this.aPos;
        this.lastPos = this.bPos;
    }

    void Update()
    {
        this.distToTarget = Vector3.Distance(this.gameObject.transform.position, this.nextPos);


        if (this.distToTarget > 0.03f)
        {
            Vector3 dir = this.nextPos - this.gameObject.transform.position;
            dir = dir.normalized * this.speed * Time.deltaTime;

            //float evaluationTime = (1 / this.maxDistance) * (this.maxDistance - (this.distToTarget - this.speed));
            //Debug.Log(evaluationTime);
            this.gameObject.transform.position += dir;
            //Vector3 newPos = Vector3.Lerp(this.lastPos, this.nextPos, this.speedAnimationCurve.Evaluate(evaluationTime));
            //this.gameObject.transform.position = newPos;

            if (this.targetPassanger != null)
                this.targetPassanger.transform.position += dir;
            if (this.soundEmitter != null)
                this.soundEmitter.SetActive(true);
        }
        else if ( this.distToTarget < 0.03f)
        {
            this.gameObject.transform.position = this.nextPos;
            if (this.soundEmitter != null)
                this.soundEmitter.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        this.ToggleNextTarget();
        this.targetPassanger = other.gameObject;
    }

    void OnTriggerExit(Collider other)
    {
        this.CheckIsArrived();
        this.targetPassanger = null;
    }

    void ToggleNextTarget()
    {
        if (this.nextPos == this.aPos)
        {
            this.nextPos = this.bPos;
            this.lastPos = this.aPos;
        }
        else
        {
            this.nextPos = this.aPos;
            this.lastPos = this.bPos;
        }
    }

    void CheckIsArrived()
    {
        this.distToTarget = Vector3.Distance(this.gameObject.transform.position, this.nextPos);

        if (this.distToTarget > 0.1f)
        {
            if (this.aFallResetPos)
            {
                this.nextPos = aPos;
                this.lastPos = bPos;
            }
            else if (this.bFallResetPos)
            {
                this.nextPos = bPos;
                this.lastPos = aPos;
            }
            else
                this.ToggleNextTarget();
        }
    }
}
