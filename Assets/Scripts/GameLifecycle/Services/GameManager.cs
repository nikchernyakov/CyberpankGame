using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Player player;
    public UIManager uiManager;
    public Transform respawnPoint;

    private GameStateEnum gameState = GameStateEnum.Game;

    private int currentUIHp = 0;
    private float fps;

	// Use this for initialization
	void Start () {
        UpdateUIHp();
	}
	
	// Update is called once per frame
	void Update () {
		if(player.GetCurrentHP() != currentUIHp)
        {
            UpdateUIHp();
            if (!player.IsAlive())
            {
                //SceneManager.LoadScene("Main");
            }
        }

        CheckOtherActions();

        fps = 1.0f / Time.deltaTime;
    }

    // Update HP value in UI
    void UpdateUIHp()
    {
        if (uiManager == null) return;
        
        currentUIHp = player.GetCurrentHP();
        uiManager.ChangeHP(currentUIHp);
    }

    private void CheckOtherActions()
    {
        // Pause check
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gameState == GameStateEnum.Game)
            {
                Time.timeScale = 0;
                gameState = GameStateEnum.Pause;
            }
            else
            {
                Time.timeScale = 1;
                gameState = GameStateEnum.Game;
            }
        }

        // Game restart check
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.LoadScene("Main");
        }

        // Game exit check
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void OnGUI()
    {
        GUILayout.Label("FPS = " + Mathf.CeilToInt(fps));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Enter " + collision.tag);
        if (collision.tag.Equals(TagManager.GetTagNameByEnum(TagEnum.Room)))
        {
            //Debug.Log("EnterRoom");
            Room room = collision.GetComponent<Room>();
            if(room == null)
            {
                Debug.LogError("Object without Room component has Room tag" + collision);
                return;
            }

            room.EnterRoom();
        }
    }
}
