using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPointTrigger : MonoBehaviour {
    public GameObject targetRespawnPoint;

    void OnTriggerEnter(Collider other)
    {
        if (this.targetRespawnPoint != null)
            GlobalVariables.respawnPoint = this.targetRespawnPoint;
    }
}
