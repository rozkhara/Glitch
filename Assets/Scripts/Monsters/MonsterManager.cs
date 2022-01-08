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

    }

    void FixedUpdate()
    {
        var monsterPosition = transform.position;
        var playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        var norm = (monsterPosition - playerPosition).normalized;
        if (Vector2.Distance(monsterPosition, playerPosition) <= 2.0f)
        {
            transform.position -= norm * Time.deltaTime;
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Weapon"))
        {
            health -= GameObject.FindGameObjectWithTag("Weapon").GetComponentInChildren<WeaponManager>().damage;
            Debug.Log("Monster Health : " + health);
            GameObject.FindGameObjectWithTag("Weapon").GetComponentInChildren<WeaponManager>().damage = 0;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
