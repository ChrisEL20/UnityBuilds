using UnityEngine;
using System.Collections;

public class Mouselook : MonoBehaviour
{

    public GameObject objectX;
    public GameObject objectY;
    public bool enable;
    public float sensitivityX;
    public float sensitivityY;

    public float maxRotationX;
    public float minRotationX;

    public Camera mainCamera;
    public float zoomSpeed;
    [Range(0, 1)]
    public float currentZoom;
    public bool invertAxis;
    public float minZoom;
    public float maxZoom;
    public LayerMask cameraCollusionLayers;


    public Mouselook()
    {
        this.objectX = null;
        this.objectY = null;
        this.enable = true;
        this.sensitivityX = 1f;
        this.sensitivityY = 1f;

        this.maxRotationX = 80f;
        this.minRotationX = -10f;

        this.mainCamera = null;
        this.zoomSpeed = 1f;
        this.currentZoom = 1f;
        this.invertAxis = true;
        this.minZoom = 1f;
        this.maxZoom = 5f;
    }
    void Start()
    {
        if (this.enable == true)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            this.SetCameraZoom();
        }
    }

    void Update()
    {
        if (Application.isMobilePlatform)
            return;

        if (this.enable == true)
        {
            float valueX = Input.GetAxis("Mouse Y") * this.sensitivityX;
            float valueY = Input.GetAxis("Mouse X") * this.sensitivityY;

            if (this.objectX && (valueX > 0 && (this.objectX.transform.localRotation.x * 100) < this.maxRotationX) || (valueX < 0 && (this.objectX.transform.localRotation.x * 100) > this.minRotationX))
                this.objectX.transform.Rotate(valueX, 0, 0);

            if (this.objectY)
                this.objectY.transform.Rotate(0, valueY, 0);

            if (this.mainCamera)
            {
                var scrollInput = Input.GetAxis("Mouse ScrollWheel");
                if (scrollInput != 0)
                {
                    if (this.invertAxis == true)
                        this.currentZoom += scrollInput * -1 * zoomSpeed;
                    else if (this.invertAxis == false)
                        this.currentZoom += scrollInput * zoomSpeed;

                    this.currentZoom = Mathf.Clamp(this.currentZoom, 0, 1);
                    this.SetCameraZoom();
                }

                RaycastHit hit;
                float currentDistance = Mathf.Lerp(this.minZoom, this.maxZoom, this.currentZoom);
                if (Physics.Raycast(this.objectY.transform.position, this.transform.TransformDirection(Vector3.back), out hit, currentDistance, this.cameraCollusionLayers))
                {
                    this.mainCamera.gameObject.transform.localPosition = new Vector3(0, 0, (hit.distance - 0.3f) * -1f);
                }
                else
                    this.SetCameraZoom();
            }
        }
    }


    void SetCameraZoom()
    {
        if (mainCamera != null)
            this.mainCamera.gameObject.transform.localPosition = new Vector3(0, 0, Mathf.Lerp(this.minZoom, this.maxZoom, this.currentZoom) * -1);
    }
}
