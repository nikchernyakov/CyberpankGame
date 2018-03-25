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
	
    public SpriteRenderer GetSpriteRenderer()
    {
        return spriteRenderer;
    }

    public Animator GetAnimator()
    {
        return animator;
    }

    public void SetTrigger(string trigger)
    {
        animator.SetTrigger(trigger);
    }

    public void SetInteger(string key, int value)
    {
        animator.SetInteger(key, value);
    }

    public void SetBool(string key, bool value)
    {
        animator.SetBool(key, value);
    }

    public bool GetBool(string key)
    {
        return animator.GetBool(key);
    }


}
