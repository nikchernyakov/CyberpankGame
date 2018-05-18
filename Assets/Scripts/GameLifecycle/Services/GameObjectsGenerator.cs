using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectsGenerator : MonoBehaviour {

    [SerializeField]
    private GameObject objectForGenerate;

    [SerializeField]
    private Transform placeForGenerate;

    public void Generate()
    {
            Instantiate(objectForGenerate, placeForGenerate);
    }
}
