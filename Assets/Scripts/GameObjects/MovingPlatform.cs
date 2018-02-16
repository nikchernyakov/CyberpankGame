using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public float platformSpeed;
    public float pointStopTime;
    private float currentPointStopTime;
    public float pointStopRadius = 0.1f;
    public List<Transform> points;
    public int nextPointInd = 0;

    public bool isMovingRight = true;
    private bool isStopped = false;

    private Transform fromPoint;
    private Transform toPoint;
    private Rigidbody2D rb;

    private List<GameObject> targets;
    private Vector3 previousPosition;

    void Start () {
        if (points.Count < 2)
        {
            Debug.LogError("MovingPlatform need two or more point's element in List");
        }

        rb = GetComponent<Rigidbody2D>();
        toPoint = points[nextPointInd];
        Stop();

        targets = new List<GameObject>();
        previousPosition = transform.position;
	}
	
	void Update () {
        CheckPoint();

        if (isStopped)
        {
            if(currentPointStopTime <= 0)
            {
                NextPoint();
                isStopped = false;
            }
            else
            {
                currentPointStopTime -= Time.deltaTime;
            }
        }

        
	}

    private void LateUpdate()
    {
        UpdateTargets();
    }

    void NextPoint()
    {
        if ((isMovingRight && nextPointInd + 1 > points.Count - 1)
            || (!isMovingRight && nextPointInd - 1 < 0))
        {
            isMovingRight = !isMovingRight;
            NextPoint();
            return;
        }


        nextPointInd += isMovingRight ? 1 : -1;
        fromPoint = toPoint;
        toPoint = points[nextPointInd];

        MoveToPoint();
    }

    void MoveToPoint()
    {
        Vector3 direction = toPoint.position - fromPoint.position;
        direction.Normalize();

        rb.velocity = direction * platformSpeed;
    }

    void CheckPoint()
    {
        if (!isStopped && MathHandler.IsPointInRange(transform.position, toPoint.position, pointStopRadius))
        {
            Stop();
        }
    }

    void Stop()
    {
        isStopped = true;
        rb.velocity = Vector3.zero;
        currentPointStopTime = pointStopTime;
    }

    private void UpdateTargets()
    {
        foreach(GameObject target in targets)
        {
            target.transform.position += transform.position - previousPosition;
        }

        previousPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        targets.Add(collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        targets.Remove(collision.gameObject);
    }

}
