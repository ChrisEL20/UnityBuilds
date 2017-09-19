using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour {

    public GameManager gameManagerComp;

    public void OnTriggerEnter(Collider other)
    {
        if (this.gameManagerComp != null)
        {
            this.gameManagerComp.StopTimer();
        }
    }
}
