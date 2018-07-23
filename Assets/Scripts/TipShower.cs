using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipShower : ObjectShower {
    [SerializeField]
    private float timer;

    private float a;
    private bool isActive = false;

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            if (!isActive)
            {
                a = gameObject.GetComponent<SpriteRenderer>().color.a;
                a += Time.deltaTime;
                gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, a);
                if (a >= 1)
                    isActive = true;
            }

            if (isActive)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    a -= Time.deltaTime;
                    gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, a);
                    if (a <= 0)
                        gameObject.SetActive(false);
                }
            }
        }
            
    }
}
