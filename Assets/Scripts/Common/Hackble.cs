﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hackble : Doneble {

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

    // Update is called once per frame
    void Update()
    {

    }

    public void Hack()
    {
        if (isHacked) return;

        isHacked = !isHacked;
        spriteRenderer.sprite = isHacked ? hackSprite : notHackSprite;

        UpdateDone();
    }

    public override bool IsDone()
    {
        return isHacked;
    }
}
