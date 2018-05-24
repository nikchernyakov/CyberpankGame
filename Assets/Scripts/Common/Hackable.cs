using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hackable : Doneble {

    public Sprite notHackSprite;
    public Sprite hackSprite;

    public bool isHacked = false;

    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = isHacked ? hackSprite : notHackSprite;
    }

    public void Hack()
    {
        UpdateDone();
    }

    protected override void ChangeDone()
    {
        if (isHacked) return;

        isHacked = !isHacked;
        spriteRenderer.sprite = isHacked ? hackSprite : notHackSprite;
    }

    public override bool IsDone()
    {
        return isHacked;
    }
}
