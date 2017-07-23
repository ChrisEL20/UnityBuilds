using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DetectionRay : MonoBehaviour {

    public LayerMask rayLayers;
    //public Image rayImage;
    public MeshRenderer reticleRenderer;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (this.reticleRenderer == null)
            return;

        RaycastHit hit;
        if (Physics.Raycast(this.gameObject.transform.position, transform.TransformDirection(Vector3.forward), out hit, 1.5f, rayLayers))
        {
            var objectMatSwitchComp = hit.collider.gameObject.GetComponent(typeof(ObjectMaterialSwitch)) as ObjectMaterialSwitch;
            if (objectMatSwitchComp != null && objectMatSwitchComp.isUIActive == false)
            {
                //this.rayImage.color = new Color(1, 1, 1, 0.5f);
                this.reticleRenderer.material.color = new Color(1, 1, 1, 0.5f);

                if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
                {
                    objectMatSwitchComp.ShowWorldUI(hit.point, this.gameObject);                    
                }
                return;
            }

            var objectSwitchButtonComp = hit.collider.GetComponent(typeof(MaterialSwitchButton)) as MaterialSwitchButton;
            if (objectSwitchButtonComp != null)
            {
                this.reticleRenderer.material.color = new Color(1, 1, 1, 0.5f);
                //this.rayImage.color = new Color(1, 1, 1, 0.5f);
                if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
                    objectSwitchButtonComp.SwitchMaterial();
                return;
            }

            var openDoorComp = hit.collider.GetComponent(typeof(OpenDoor)) as OpenDoor;
            if (openDoorComp != null && openDoorComp.canOpen == true)
            {
                this.reticleRenderer.material.color = new Color(1, 1, 1, 0.5f);
                if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
                    openDoorComp.ToggleDoor();
            }
            else
                this.reticleRenderer.material.color = new Color(1, 1, 1, 0f);
        }
        else
        {
            //this.rayImage.color = new Color(1, 1, 1, 0f);
            this.reticleRenderer.material.color = new Color(1, 1, 1, 0f);
        }
	}
}
