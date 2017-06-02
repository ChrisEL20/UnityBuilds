using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBox : MonoBehaviour {

    public Vector2 moveDirection;
    public float movementSpeed;

    [Range(1,3)]
    public int scaleLevel;
    public bool canMove;

    public BounceBox()
    {
        this.moveDirection = Vector2.left;
        this.movementSpeed = 2f;

        this.scaleLevel = 1;
        this.canMove = true;
    }

    private Rigidbody2D boxRigidbody;

    void OnEnable()
    {
        this.boxRigidbody = this.gameObject.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        this.SetScale();
    }
	
	void FixedUpdate () {
        if (this.boxRigidbody && this.canMove)
            this.boxRigidbody.MovePosition(this.boxRigidbody.position + (this.moveDirection.normalized * this.movementSpeed * Time.fixedDeltaTime));
	}


    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController otherPlayer = other.gameObject.GetComponent(typeof(PlayerController)) as PlayerController;

        if (otherPlayer)
        {
            if (otherPlayer.scaleLevel >= this.scaleLevel)
                this.ChangeDirection(other);
            else
                this.canMove = false;
        }
        else
            this.ChangeDirection(other);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        PlayerController otherPlayer = other.gameObject.GetComponent(typeof(PlayerController)) as PlayerController;
        if (otherPlayer)
        {
            if (otherPlayer.scaleLevel < this.scaleLevel)
            {
                if (this.ContactInMoveDirection(other))
                    this.canMove = false;
                else
                    this.canMove = true;
            }
            else
            {
                this.canMove = true;
                this.ChangeDirection(other);
            }
        }
        else
        {
            this.canMove = true;
            this.ChangeDirection(other);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        PlayerController otherPlayer = other.gameObject.GetComponent(typeof(PlayerController)) as PlayerController;

        if (otherPlayer)
        {
            if (otherPlayer.scaleLevel < this.scaleLevel)
                this.canMove = true;
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

    void ChangeDirection(Collision2D other)
    {
        this.canMove = true;
        Vector3 oldMoveDirection = this.moveDirection;
        this.moveDirection = other.contacts[0].normal;
        if (oldMoveDirection.x != this.moveDirection.x && oldMoveDirection.y != this.moveDirection.y)
            this.SnapToGrid();
    }

    bool ContactInMoveDirection(Collision2D coll)
    {
        if (this.moveDirection == coll.contacts[0].normal * -1)
            return true;
        else
            return false;
    }

    void SnapToGrid()
    {
        Vector2 pos = this.gameObject.transform.position;

        float roundetX = Mathf.Round(pos.x);
        float roundetY = Mathf.Round(pos.y);

        this.gameObject.transform.position = new Vector3(roundetX, roundetY, 0);
    }
}
