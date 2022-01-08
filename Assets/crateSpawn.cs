using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crateSpawn : MonoBehaviour
{
    public List<GameObject> Crates;
    private GM gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GMobject").GetComponent<GM>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.I)){
            gameManager.ClearTimeCheckEnd();
            //if clearTime under something time
            if (gameManager.clearTime <= 30.0f)
            {
                Instantiate(Crates[2], transform.position, Quaternion.identity);
            }
            else if (gameManager.clearTime <= 60.0f)
            {
                Instantiate(Crates[1], transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(Crates[0], transform.position, Quaternion.identity);
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameManager.isOnPause)
        {
            if (collision.gameObject.tag == "Player")
            {
                gameManager.ClearTimeCheckEnd();
                //if clearTime under something time
                if (gameManager.clearTime <= 30.0f)
                {
                    Instantiate(Crates[2], transform.position, Quaternion.identity);
                }
                else if (gameManager.clearTime <= 60.0f)
                {
                    Instantiate(Crates[1], transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(Crates[0], transform.position, Quaternion.identity);
                }

            }
        }
        
    }
    
}
