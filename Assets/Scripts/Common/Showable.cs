using UnityEngine;

/*
 * Script for show or hiding object
 */
public class Showable : MonoBehaviour {

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }
}
