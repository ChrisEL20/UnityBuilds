using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    public PlayerController.PlayerType targetType;

    public bool isActive;

    private List<GameObject> triggerTargets;

    public Button()
    {
        this.targetType = PlayerController.PlayerType.Amaterasu;
        this.isActive = false;

        this.triggerTargets = new List<GameObject>();
    }

    void Update()
    {
        this.CheckTarget();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (this.triggerTargets.Contains(other.gameObject) == false)
            this.triggerTargets.Add(other.gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (this.triggerTargets.Contains(other.gameObject))
            this.triggerTargets.Remove(other.gameObject);
    }

    void CheckTarget()
    {
        if (this.triggerTargets.Count == 0)
            this.isActive = false;
        else
        {
            if (this.targetType == PlayerController.PlayerType.None)
                this.isActive = true;
            else
            {
                foreach(GameObject obj in triggerTargets)
                {
                    PlayerController playerComp = obj.GetComponent(typeof(PlayerController)) as PlayerController;
                    if (playerComp != null)
                    {
                        if (playerComp.type == this.targetType)
                        {
                            this.isActive = true;
                            return;
                        }
                    }
                }
                this.isActive = false;
            }
        }
    }
	
}
