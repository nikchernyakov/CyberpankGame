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

    public void ChangeButton()
    {
        UpdateDone();
    }

    protected override void ChangeDone()
    {
        if (!isClickable) return;

        if (hasOnePush)
            isClickable = false;

        isOn = !isOn;
        spriteRenderer.sprite = isOn ? buttonOn : buttonOff;
    }

    public override bool IsDone()
    {
        return isOn;
    }

}
