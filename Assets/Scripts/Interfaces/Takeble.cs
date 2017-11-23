using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Takeble : MonoBehaviour{

    private bool isTaken = false;
    public bool IsTaken()
    {
        return isTaken;
    }

    public virtual void Take(Transform whoTakes)
    {
        isTaken = true;
        transform.SetParent(whoTakes);
    }

    public virtual void Drop()
    {
        isTaken = false;
        transform.SetParent(null);
    }

}
