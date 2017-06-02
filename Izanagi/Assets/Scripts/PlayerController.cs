using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Range(1, 3)]
    public int scaleLevel;
    [Range(1,3)]
    public int speedLevel;


    public float movementSpeed;
    public bool movementInverted;

    private bool canMove;

    public enum PlayerType { Amaterasu, Tsukuyomi, Susano, None};
    public PlayerType type;

    private Rigidbody2D characterRigidbody;

    private float lastLevelSwitchTime;
    private float levelSwitchCoolDown;

    private Vector3 contactDirection;

    public PlayerController()
    {
        this.scaleLevel = 2;
        this.speedLevel = 2;

        this.movementSpeed = 2f;
        this.movementInverted = false;

        this.canMove = true;

        this.type = PlayerType.Amaterasu;
        this.contactDirection = new Vector3(0, 0, 0);

        this.levelSwitchCoolDown = 0.5f;
    }
    
    public void OnEnable()
    {
        this.characterRigidbody = this.gameObject.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        this.SetScale();
        this.lastLevelSwitchTime = Time.time - this.levelSwitchCoolDown;
    }

	void FixedUpdate ()
    {
        if (!this.canMove)
            return;

        float currentMovementSpeed = this.movementSpeed * this.speedLevel;
        if (this.movementInverted)
            currentMovementSpeed *= -1;

        if (Input.GetAxis("Horizontal") != 0)
        {
            Vector3 moveDir = Vector2.right * Input.GetAxis("Horizontal") * currentMovementSpeed;
            moveDir = moveDir.normalized;
            if (moveDir != this.contactDirection)
                this.characterRigidbody.MovePosition(characterRigidbody.position + Vector2.right * Input.GetAxis("Horizontal") * currentMovementSpeed * Time.deltaTime);
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            Vector3 moveDir = Vector2.up * Input.GetAxis("Vertical") * currentMovementSpeed;
            moveDir = moveDir.normalized;
            if (moveDir != this.contactDirection)
                this.characterRigidbody.MovePosition(characterRigidbody.position + Vector2.up * Input.GetAxis("Vertical") * currentMovementSpeed * Time.deltaTime);
        }
    }

    public void ToggleInvertBool()
    {
        if (this.movementInverted)
            this.movementInverted = false;
        else
            this.movementInverted = true;
    }

    public void SnapToEndTarget(Vector3 pos)
    {
        this.scaleLevel = 2;
        this.SetScale();
        this.transform.position = pos;
        this.canMove = false;
        this.characterRigidbody.bodyType = RigidbodyType2D.Kinematic;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        this.contactDirection = other.contacts[0].normal * -1;
        PlayerController otherPlayer = other.gameObject.GetComponent(typeof(PlayerController)) as PlayerController;
        if (otherPlayer!= null)
        {
            if (this.levelSwitchCoolDown + this.lastLevelSwitchTime < Time.time && otherPlayer.levelSwitchCoolDown + otherPlayer.lastLevelSwitchTime < Time.time && this.canMove && otherPlayer.canMove)
            {
                print(this.gameObject.name + " collides with " + otherPlayer.gameObject.name);
                this.lastLevelSwitchTime = Time.time;
                otherPlayer.lastLevelSwitchTime = Time.time;
                this.SwitchLevels(otherPlayer);
            }

        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        this.contactDirection = new Vector3(0,0,0);
    }

    void SetScale()
    {
        switch (this.scaleLevel){
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

    void SwitchLevels(PlayerController other)
    {
        int tempScaleLevel = this.scaleLevel;
        int tempSpeedLevel = this.speedLevel;

        this.scaleLevel = other.scaleLevel;
        this.speedLevel = other.speedLevel;

        other.scaleLevel = tempScaleLevel;
        other.speedLevel = tempSpeedLevel;

        this.SetScale();
        other.SetScale();
    }
}
