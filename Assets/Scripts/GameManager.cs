using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Player player;
    public UIManager uiManager;

    private int currentHp;

	// Use this for initialization
	void Start () {
        ChangeHp();
	}
	
	// Update is called once per frame
	void Update () {
		if(player.hp != currentHp)
        {
            ChangeHp();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

    void ChangeHp()
    {
        currentHp = player.hp;
        uiManager.ChangeHP(currentHp);
    }
}
