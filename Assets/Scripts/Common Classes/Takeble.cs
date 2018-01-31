using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Takeble : MonoBehaviour{

    private bool isTaken = false;
    private Transform previousParent;

    public bool IsTaken()
    {
        return isTaken;
    }

    public virtual void Take(Transform whoTakes)
    {
        isTaken = true;
        previousParent = transform.parent;
        transform.SetParent(whoTakes);
    }

    public virtual void Drop()
    {
        isTaken = false;
        transform.SetParent(previousParent);
    }

}
