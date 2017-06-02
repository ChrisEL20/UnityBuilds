using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject targetCamera;

    public float followingSpeed;

    public GameObject[] followingCharacters;
    private float cameraZPos;

    public CameraController()
    {
        this.followingSpeed = 2f;
    }

    void OnEnable()
    {
        if (this.targetCamera)
            this.cameraZPos = this.targetCamera.transform.position.z;
    }
	
	void FixedUpdate () {
        if (this.targetCamera == null)
            return;

        this.gameObject.transform.position = this.CenterPosition();
        Vector3 newCameraPos = Vector3.Lerp(this.gameObject.transform.position, this.targetCamera.transform.position, this.followingSpeed * Time.deltaTime);
        newCameraPos.z = this.cameraZPos;
        this.targetCamera.transform.position = newCameraPos;
    }

    Vector3 CenterPosition()
    {
        Vector3 center = new Vector3(0, 0, 0);

        for (int i = 0; i < this.followingCharacters.Length; i++)
        {
            center += this.followingCharacters[i].transform.position;
        }

        center = center / this.followingCharacters.Length;

        return center;
    }
}
