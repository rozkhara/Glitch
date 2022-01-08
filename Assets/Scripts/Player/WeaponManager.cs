using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    public float damage;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Monster"))
        {
            Debug.Log("Slash");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    // void onTriggerExit2D(Collider2D collision)
    // {
    //     if (collision.transform.CompareTag("Monster"))
    //         gameObject.GetComponent<BoxCollider2D>().enabled = false;
    // }
}
