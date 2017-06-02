using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementInverter : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController otherPlayer = other.gameObject.GetComponent(typeof(PlayerController)) as PlayerController;
        if (otherPlayer)
        {
            otherPlayer.ToggleInvertBool();
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.2f);
            Destroy(this);
        }
    }
}
