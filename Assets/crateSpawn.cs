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
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GM>();

    }

    // Update is called once per frame
    void Update()
    {
        

        
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
                    Instantiate(Crates[2], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    Destroy(this.gameObject);

                }
                else if (gameManager.clearTime <= 60.0f)
                {
                    Instantiate(Crates[1], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    Destroy(this.gameObject);

                }
                else
                {
                    Instantiate(Crates[0], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    Destroy(this.gameObject);

                }

            }
        }
        
    }
    
}
