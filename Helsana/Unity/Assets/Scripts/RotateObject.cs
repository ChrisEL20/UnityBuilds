using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {

    public Vector3 rotationVector;
    public float movementSpeed = 5f;

	void Update () {

        this.gameObject.transform.Rotate(this.rotationVector * Time.deltaTime * this.movementSpeed);
	}
}
