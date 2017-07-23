using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {

    public bool canOpen;
    public GameObject colliderObject;
    public Animator animatorComp;

    private bool isOpen;
    private float lastOpeningTime;

    public OpenDoor()
    {
        this.canOpen = true;
        this.colliderObject = null;
        this.animatorComp = null;

        this.isOpen = false;
    }
    
	
	// Update is called once per frame
	void Update () {
        if (this.isOpen && this.lastOpeningTime + 10f < Time.time)
        {
            this.ToggleDoor();
        }
	
	}

    public void ToggleDoor()
    {
        if (this.colliderObject != null && this.animatorComp && this.canOpen)
        {
            if (this.isOpen)
            {
                this.isOpen = false;
                this.animatorComp.SetBool("isOpen", this.isOpen);
                this.colliderObject.SetActive(true);                

            }
            else if (!this.isOpen)
            {
                this.isOpen = true;
                this.animatorComp.SetBool("isOpen", this.isOpen);
                this.colliderObject.SetActive(false);
                this.lastOpeningTime = Time.time;           
            }
        }

    }
}
