using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour
{
    GM gameManager;
    public LoadScene loadScene;
    public void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GM>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!gameManager.isOnPause)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    gameManager.ClearTimeCheckStart();
                    loadScene.LoadSceneA("PlayableScene");
                }

            }
        }

    }
}
