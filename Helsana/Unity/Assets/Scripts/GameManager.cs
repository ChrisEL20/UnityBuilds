using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool timerIsStarted;

    public float totalPlayTime;
    [HideInInspector]
    public float currentPlayTime;
    private float lastSecondDownTime;

    private bool resetTimerstarted;

    private BasicUI basicUIComp;
    private float startTouchTime;

    public BridgeCrashController crashController;
    public Animator crashAnimator;

    private bool restartTimerStarted;
    private float restartTimerStartTime;


    public GameManager()
    {
        this.timerIsStarted = false;

        this.totalPlayTime = 180f;
        this.currentPlayTime = 180f;
        this.restartTimerStarted = false;
    }
	
    void OnEnable()
    {
        this.basicUIComp = GameObject.FindGameObjectWithTag("BasicUI").GetComponent(typeof(BasicUI)) as BasicUI;
    }

	void Update () {
		if (Input.GetKeyDown(KeyCode.R))
        {
            GlobalVariables.ReloadScene();
        }

        if (this.restartTimerStarted && this.restartTimerStartTime + 10 < Time.time)
            GlobalVariables.ReloadScene();


        if (Input.GetButton("Fire1"))
        {
            //Touch touch = Input.GetTouch(0);

            if (!resetTimerstarted)
            {
                this.startTouchTime = Time.time;
                resetTimerstarted = true;
            }

            if (this.startTouchTime + 5f < Time.time)
                GlobalVariables.ReloadScene();
        }

        else
        {
            resetTimerstarted = false;
        }

        if (this.timerIsStarted)
        {
            if (this.lastSecondDownTime + 1 < Time.time)
            {
                this.currentPlayTime--;
                this.lastSecondDownTime = Time.time;
                if (this.basicUIComp != null)
                    this.basicUIComp.UpdateTimerImage();

                if (this.totalPlayTime - this.currentPlayTime > 60 && this.crashController != null)
                    this.crashController.ActivateCrash(this.crashAnimator);

                if (this.currentPlayTime <= 0)
                    this.TimeIsOver();
            }
        }
	}

    public void StartTimer()
    {
        this.timerIsStarted = true;
        this.lastSecondDownTime = Time.time;
        this.currentPlayTime = this.totalPlayTime;
        if (this.basicUIComp != null)
            this.basicUIComp.UpdateTimerImage();
    }

    public void StopTimer()
    {
        this.restartTimerStartTime = Time.time;
        this.restartTimerStarted = true;
        this.timerIsStarted = false;
        if (this.basicUIComp != null)
            this.basicUIComp.ShowEndLevelUI();
    }

    public void TimeIsOver()
    {
        this.restartTimerStartTime = Time.time;
        this.restartTimerStarted = true;
        this.timerIsStarted = false;
        if (this.basicUIComp != null)
            this.basicUIComp.TimeIsOversUI();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!this.timerIsStarted)
            this.StartTimer();
    }
}
