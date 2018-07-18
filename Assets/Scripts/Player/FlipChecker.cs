using UnityEngine;

public class FlipChecker : MonoBehaviour
{
    
    public Transform ZRotate; // Rotate's object for axis Z
    
    // Rotation bounds
    public float MinAngle = 0;
    public float MaxAngle = 0;

    private bool _facingRight = false; // For checking what side is turned
    private float _angle;
    private int _invert;
    private Vector3 _mouse;
    
    // Use this for initialization
    void Start()
    {
        if (!_facingRight)
        {
            _invert = -1;
        }
        else
        {
            _invert = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ZRotate)
        {
            SetRotation();
        }
    }
    
    public bool IsFacingRight()
    {
        return _facingRight;
    }
    
    public void CheckFlip(float move)
    {
        if (move > 0 && !_facingRight)
            Flip();
        else if (move < 0 && _facingRight)
            Flip();
        /*if (angle == maxAngle && mouse.x < zRotate.position.x && facingRight) Flip();
        else if (angle == maxAngle && mouse.x > zRotate.position.x && !facingRight) Flip();*/
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void SetRotation()
    {
        var mousePosMain = Input.mousePosition;
        mousePosMain.z = Camera.main.transform.position.z;
        _mouse = Camera.main.ScreenToWorldPoint(mousePosMain);
        var lookPos = _mouse - transform.position;
        _angle = Mathf.Atan2(lookPos.y, lookPos.x * _invert) * Mathf.Rad2Deg;
        _angle = Mathf.Clamp(_angle, MinAngle, MaxAngle);
        ZRotate.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
    }
}