using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour {

    private Collider2D targetCollider;

    public Button[] activaterButtons;

    public bool isOpen;

    private SpriteRenderer spriteRenderComp;
    public Sprite closedSprite;
    public Sprite openSprite;

    public Barrier()
    {
        this.targetCollider = null;
        this.activaterButtons = new Button[0];
        this.isOpen = false;
    }
    
	void Start () {
        this.targetCollider = this.gameObject.GetComponent(typeof(Collider2D)) as Collider2D;
        this.spriteRenderComp = this.gameObject.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
	}
	
	void Update () {
        this.isOpen = this.CheckAllButtonsActive();
        this.SetSprite();

        if (this.isOpen)
        {
            this.targetCollider.enabled = false;
        }
        else
        {
            this.targetCollider.enabled = true;
        }
	}

    public bool CheckAllButtonsActive()
    {
        int counter = 0;

        foreach (Button b in activaterButtons)
        {
            if (b.isActive)
                counter += 1;
        }
        if (counter < this.activaterButtons.Length)
            return false;
        else
            return true;
    }

    public void SetSprite()
    {
        if (this.spriteRenderComp != null && this.openSprite && this.closedSprite)
        {
            if (this.isOpen)
                this.spriteRenderComp.sprite = this.openSprite;
            else
                this.spriteRenderComp.sprite = this.closedSprite;
        }
    }
}
