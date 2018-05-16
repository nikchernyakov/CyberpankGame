using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectsGenerator : MonoBehaviour {

    [SerializeField]
    private GameObject objectForGenerate;

    [SerializeField]
    private List<Transform> placesForGenerate;

    public void Generate()
    {
        foreach (Transform place in placesForGenerate)
            Instantiate(objectForGenerate, place);
    }
}
