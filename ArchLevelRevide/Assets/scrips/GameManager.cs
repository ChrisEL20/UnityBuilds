using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class GameManager : MonoBehaviour {

    public int clickCounter;
    private float lastClickTime;

    public GameObject targetCamera;

	void Start()
    {
        this.clickCounter = 0;
        this.lastClickTime = 0;
    }

	void Update () {
		
        if (this.lastClickTime + 0.4f < Time.time && this.clickCounter > 0)
        {
            this.clickCounter = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            this.clickCounter++;
            this.lastClickTime = Time.time;

            if (this.clickCounter >= 7)
                this.ToggleVRMode();
        }

        if (!VRSettings.enabled && this.targetCamera != null)
            this.targetCamera.transform.localRotation = InputTracking.GetLocalRotation(VRNode.CenterEye);

    }

    void ToggleVRMode()
    {
        this.clickCounter = 0;
        Debug.Log(VRSettings.enabled);
        if (VRSettings.enabled)
        {
            VRSettings.enabled = false;
            Debug.Log("VR-Mode disabled");
        }
        else
        {
            VRSettings.enabled = true;
            Debug.Log("VR-Mode enabled");
        }
    }
}
