using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DetectionRay : MonoBehaviour {

    public LayerMask rayLayers;
    public Image rayImage;

    public int timer = 0;
    private float lastTimerUpTime;

    public Sprite[] courserSprites;
    
	void Update () {

        RaycastHit hit;
        if (Physics.Raycast(this.gameObject.transform.position, transform.TransformDirection(Vector3.forward), out hit, 2f, rayLayers))
        {
            ObjectMaterialSwitch objectMatSwitchComp = hit.collider.gameObject.GetComponent(typeof(ObjectMaterialSwitch)) as ObjectMaterialSwitch;
            if (objectMatSwitchComp != null && objectMatSwitchComp.isUIActive == false)
            {
                this.FadeInCourser();
                this.TimerUpStep();

                if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0) || this.timer >= 3)
                {
                    objectMatSwitchComp.ShowWorldUI(hit.point, this.gameObject);
                    this.timer = 0;
                }
                return;
            }

            MaterialSwitchButton objectSwitchButtonComp = hit.collider.GetComponent(typeof(MaterialSwitchButton)) as MaterialSwitchButton;
            if (objectSwitchButtonComp != null)
            {
                this.FadeInCourser();
                this.TimerUpStep();

                if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0) || this.timer >= 3)
                {
                    objectSwitchButtonComp.SwitchMaterial();
                    this.timer = 0;
                }
                return;
            }

            OpenDoor openDoorComp = hit.collider.GetComponent(typeof(OpenDoor)) as OpenDoor;
            if (openDoorComp != null && openDoorComp.canOpen == true)
            {
                this.FadeInCourser();
                this.TimerUpStep();

                if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0) || this.timer >= 3)
                {
                    this.timer = 0;
                    if (openDoorComp.twoWingedDoor != null)
                        openDoorComp.twoWingedDoor.ToggleDoor();
                    openDoorComp.ToggleDoor();
                }
            }
            else
            {
                this.FadeOutCourser();
                this.timer = 0;
            }
        }
        //Hide Curser
        else
        {
            this.FadeOutCourser();
            this.timer = 0;
        }
	}

    void FadeOutCourser()
    {
        if (this.rayImage != null && this.courserSprites.Length > 0)
        {
            //this.rayImage.sprite = this.courserSprites[0];
            if (this.rayImage.color.a < 0f)
            {
                this.rayImage.color = new Color(1, 1, 1, 0f);
            }
            else
            {
                Color newColor = this.rayImage.color;
                newColor.a -= 0.03f;
                this.rayImage.color = newColor;
            }
        }
    }

    void FadeInCourser()
    {
        if (this.rayImage != null && this.courserSprites.Length > 0)
        {
            if (this.rayImage.color.a > 0.5f)
            {
                this.rayImage.color = new Color(1, 1, 1, 0.5f);
            }
            else
            {
                Color newColor = this.rayImage.color;
                newColor.a += 0.03f;
                this.rayImage.color = newColor;
            }
        }

    }
    

    void TimerUpStep()
    {
        if (this.timer >= 3)
            this.timer = 0;

        if (this.lastTimerUpTime + 1 < Time.time)
        {
            this.timer++;
            this.rayImage.sprite = this.courserSprites[this.timer];
            this.lastTimerUpTime = Time.time;
        }
    }
}
