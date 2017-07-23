using UnityEngine;
using System.Collections;

public class EnableVR : MonoBehaviour {
    
    public Mouselook mouselook;

    public EnableVR()
    {
        this.mouselook = null;
    }
    
	
	// Update is called once per frame
	void Update () {
        if (this.mouselook == null)
            return;

        if (Input.GetKeyDown(KeyCode.V) || Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(1).phase == TouchPhase.Began)
        {
        }
	}
}
