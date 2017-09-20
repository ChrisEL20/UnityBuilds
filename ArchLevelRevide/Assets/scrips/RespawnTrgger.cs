using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrgger : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (GlobalVariables.respawnPoint != null)
        {
            other.gameObject.transform.position = GlobalVariables.respawnPoint.transform.position;
            //other.gameObject.transform.rotation = GlobalVariables.respawnPoint.transform.rotation;
            Rigidbody otherRigidbody = other.gameObject.GetComponent(typeof(Rigidbody)) as Rigidbody;
            if (otherRigidbody != null)
            {
                otherRigidbody.velocity = Vector3.zero;
                otherRigidbody.angularVelocity = Vector3.zero;
            }
        }
    }
}
