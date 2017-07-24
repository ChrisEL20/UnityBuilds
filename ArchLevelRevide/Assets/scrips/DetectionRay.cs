using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DetectionRay : MonoBehaviour {

    public LayerMask rayLayers;
    public Image rayImage;
    
	void Update () {

        RaycastHit hit;
        if (Physics.Raycast(this.gameObject.transform.position, transform.TransformDirection(Vector3.forward), out hit, 2f, rayLayers))
        {
            
            ObjectMaterialSwitch objectMatSwitchComp = hit.collider.gameObject.GetComponent(typeof(ObjectMaterialSwitch)) as ObjectMaterialSwitch;
            if (objectMatSwitchComp != null && objectMatSwitchComp.isUIActive == false)
            {
                StartCoroutine(this.FadeInCourser());

                if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
                {
                    objectMatSwitchComp.ShowWorldUI(hit.point, this.gameObject);                    
                }
                return;
            }

            MaterialSwitchButton objectSwitchButtonComp = hit.collider.GetComponent(typeof(MaterialSwitchButton)) as MaterialSwitchButton;
            if (objectSwitchButtonComp != null)
            {
                StartCoroutine(this.FadeInCourser());
                if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
                    objectSwitchButtonComp.SwitchMaterial();
                return;
            }

            OpenDoor openDoorComp = hit.collider.GetComponent(typeof(OpenDoor)) as OpenDoor;
            if (openDoorComp != null && openDoorComp.canOpen == true)
            {
                StartCoroutine(this.FadeInCourser());
                if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
                    openDoorComp.ToggleDoor();
            }
            else
                StartCoroutine(this.FadeOutCourser());
        }
        //Hide Curser
        else
        {
            StartCoroutine(this.FadeOutCourser());
        }
	}

    IEnumerator FadeOutCourser()
    {
        if (this.rayImage != null)
        {
            for (int i = 0; i < 1; i += 0)
            {
                if (this.rayImage.color.a <= 0f)
                {
                    i = 1;
                    this.rayImage.color = new Color(1, 1, 1, 0f);
                }
                else
                {
                    yield return new WaitForSeconds(0.1f);
                    Color newColor = this.rayImage.color;
                    newColor.a -= 0.05f;
                    this.rayImage.color = newColor;
                }
            }
        }
    }

    IEnumerator FadeInCourser()
    {
        if (this.rayImage != null)
        {
            for (int i = 0; i < 1; i += 0)
            {
                if (this.rayImage.color.a >= 0.5f)
                {
                    i = 1;
                    this.rayImage.color = new Color(1, 1, 1, 0.5f);
                }
                else
                {
                    yield return new WaitForSeconds(0.1f);
                    Color newColor = this.rayImage.color;
                    newColor.a += 0.05f;
                    this.rayImage.color = newColor;
                }
            }
        }
    }
}
