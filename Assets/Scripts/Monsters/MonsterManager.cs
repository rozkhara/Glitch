using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{


    Rigidbody2D rigid;
    SpriteRenderer SpriteRenderer;
    // Start is called before the first frame update
    public float health;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Weapon"))
        {
            Debug.Log("Hit");
            health -= GameObject.FindGameObjectWithTag("Weapon").GetComponentInChildren<WeaponManager>().damage;
            GameObject.FindGameObjectWithTag("Weapon").GetComponentInChildren<WeaponManager>().damage = 0;
        }
    }
}
