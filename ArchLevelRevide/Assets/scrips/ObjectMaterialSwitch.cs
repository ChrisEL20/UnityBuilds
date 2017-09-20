using UnityEngine;
using System.Collections;

public class ObjectMaterialSwitch : MonoBehaviour {

    [HideInInspector]
    public bool isUIActive;
    public GameObject worldUIObject;
    public bool saveYPosition;
    private float yPos;
    private GameObject orientationObj;
    private float setActiveTime;

    public ObjectMaterialSwitch()
    {
        this.isUIActive = false;
        this.worldUIObject = null;
        this.saveYPosition = true;
        this.yPos = 0f;
        this.orientationObj = null;
        this.setActiveTime = 0f;
    }

    void Start()
    {
        if (this.worldUIObject)
        {
            this.worldUIObject.SetActive(false);
            if (this.saveYPosition)
                this.yPos = this.worldUIObject.transform.position.y;
        }
    }

    void Update()
    {
        if (this.isUIActive)
        {
            this.worldUIObject.transform.LookAt(this.orientationObj.transform);
            if (this.setActiveTime + 8f < Time.time)
                HideWorldUI();
        }
    }
    

    public void ShowWorldUI(Vector3 hitPoint, GameObject temOrientationObj)
    {
        if (this.worldUIObject != null && !this.isUIActive) {
            this.worldUIObject.SetActive(true);
            this.orientationObj = temOrientationObj;
            Vector3 pos = temOrientationObj.transform.position + temOrientationObj.transform.forward;
            pos.y = temOrientationObj.transform.position.y - 0.3f;
            this.isUIActive = true;
            if (this.saveYPosition)
                pos.y = this.yPos;
            this.worldUIObject.transform.position = pos;
            this.worldUIObject.transform.LookAt(this.orientationObj.transform.position);
            this.setActiveTime = Time.time;
        }
    }

    public void HideWorldUI()
    {
        if (this.worldUIObject != null && this.isUIActive)
        {
            this.worldUIObject.SetActive(false);
            this.isUIActive = false;
        }
    }
}
