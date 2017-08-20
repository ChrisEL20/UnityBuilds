using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frameRate : MonoBehaviour {
    void Awake() {
        Application.targetFrameRate = 90;
    }
}