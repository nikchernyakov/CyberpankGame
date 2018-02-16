using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Doneble {
    public Sprite buttonOff;
    public Sprite buttonOn;

    public bool isOn = false;
    public bool isClickable = true;
    public bool hasOnePush = true;

    
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = isOn ? buttonOn : buttonOff;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeButton()
    {
        if (!isClickable) return;

        if (hasOnePush)
            isClickable = false;

        isOn = !isOn;
        spriteRenderer.sprite = isOn ? buttonOn : buttonOff;

        UpdateDone();
    }

    public override bool IsDone()
    {
        return isOn;
    }

}
