using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRenderer : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private Animator animator;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
	}
	
    public SpriteRenderer getSpriteRenderer()
    {
        return spriteRenderer;
    }

    public Animator getAnimator()
    {
        return animator;
    }

}
