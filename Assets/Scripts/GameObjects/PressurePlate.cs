using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Doneble {

    [System.Serializable]
    public struct Plate
    {

        public Vector2 colliderSize;
        public Sprite sprite;

        public void SetSizeToCollider(BoxCollider2D collider)
        {
            collider.size = colliderSize;
        }

        public Sprite GetSprite()
        {
            return sprite;
        }
    }

    [SerializeField]
    public Plate pressedPlate;
    [SerializeField]
    public Plate emptyPlate;
    private Plate currentPlate;

    public bool isPressed = false;
    public int pressingObjectsCount = 0;
    public LayerMask whatIsPress;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D collider;

    public override bool IsDone()
    {
        return isPressed;
    }

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();

        ChangePlate(isPressed);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangePress()
    {
        isPressed = !isPressed;
        ChangePlate(isPressed);

        UpdateDone();
    }

    public void ChangePlate(bool isPressed)
    {
        currentPlate = isPressed ? pressedPlate : emptyPlate;
        currentPlate.SetSizeToCollider(collider);
        spriteRenderer.sprite = currentPlate.GetSprite();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & whatIsPress) != 0)
        {
            pressingObjectsCount++;

            if (pressingObjectsCount == 1)
            {
                ChangePress();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & whatIsPress) != 0)
        {
            pressingObjectsCount--;

            if (pressingObjectsCount <= 0)
            {
                pressingObjectsCount = 0;
                ChangePress();
            }
        }
    }
}
