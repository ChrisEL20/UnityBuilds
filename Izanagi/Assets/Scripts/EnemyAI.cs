using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    [Range(1,3)]
    public int scaleLevel;
    [Range(1,3)]
    public int speedLevel;
    public float movementSpeed;

    private PathController pathControllerComp;

    private List<PlayerController> pathTargets;

    public LayerMask rayHitLayerMask;

    public EnemyAI()
    {
        this.scaleLevel = 2;
        this.speedLevel = 2;

        this.pathTargets = new List<PlayerController>();
    }

    void OnEnable()
    {
        this.pathControllerComp = this.gameObject.GetComponent(typeof(PathController)) as PathController;
        this.SetScale();
    }
	
	void Update () {
        if (this.pathControllerComp)
        {
            this.pathControllerComp.speed = this.movementSpeed * this.speedLevel;
            if (this.pathTargets.Count > 0 && this.pathControllerComp.target == null)
            {
                for (int i = 0; i < this.pathTargets.Count; i++)
                {
                    Vector3 dirToTarget = this.pathTargets[i].transform.position - this.gameObject.transform.position;
                    dirToTarget = dirToTarget.normalized;

                    float rayLenght = Vector3.Distance(this.pathTargets[i].transform.position, this.gameObject.transform.position);

                    Debug.DrawLine(this.gameObject.transform.position, this.gameObject.transform.position + (dirToTarget * rayLenght));

                    if (!Physics2D.Raycast(this.gameObject.transform.position, dirToTarget, rayLenght, this.rayHitLayerMask) && this.pathTargets[i].scaleLevel <= this.scaleLevel)
                    {
                        print("set target");
                        this.pathControllerComp.target = this.pathTargets[i].gameObject;
                        break;
                    }
                }
            }
            else if (this.pathControllerComp.target != null && !this.pathTargets.Contains(this.pathControllerComp.target.GetComponent<PlayerController>()))
            {
                this.pathControllerComp.target = null;
                this.pathControllerComp.ResetMovementPointsList();
            }
        }
	}

    void SetScale()
    {
        switch (this.scaleLevel)
        {
            case 1:
                this.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                break;
            case 2:
                this.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                break;
            case 3:
                this.gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerComp = other.gameObject.GetComponent(typeof(PlayerController)) as PlayerController;
        if (playerComp != null)
            this.pathTargets.Add(playerComp);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        PlayerController playerComp = other.gameObject.GetComponent(typeof(PlayerController)) as PlayerController;
        if (playerComp != null)
        {
            if (this.pathTargets.Contains(playerComp))
                this.pathTargets.Remove(playerComp);
        }
    }
}
