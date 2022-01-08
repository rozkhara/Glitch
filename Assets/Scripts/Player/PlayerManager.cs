using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    public GM GameManager;
    public float maxSpeed;
    public float jumpPower;
    bool canDash = true;
    Rigidbody2D rigid;
    SpriteRenderer SpriteRenderer;
    // Start is called before the first frame update

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
        if (!GameManager.isOnPause)
        {
            //Jump
            if (Input.GetButtonDown("Jump"))
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

            //Stop Speed
            if (Input.GetButtonUp("Horizontal"))
            {
                rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
            }

            //Direction Sprite
            if (Input.GetButtonDown("Horizontal"))
            {
                SpriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                //immobile while charging attack
                maxSpeed = 0;
            }

            if (Input.GetKey(KeyCode.K))
            {
                //slows movement while chraging attack
                //                rigid.velocity = new Vector2(0,0);

                //damage increase while charing attack
                GameObject.Find("Weapon").GetComponentInChildren<WeaponManager>().damage += Time.deltaTime * 30;
            }

            if (Input.GetKeyUp(KeyCode.K))
            {
                GameObject.Find("Weapon").GetComponentInChildren<BoxCollider2D>().enabled = true;
                StartCoroutine(wait());
                //mobile after finishing attack
                maxSpeed = 2;
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                if (canDash == true)
                {
                    //dash upwards
                    // transform.position += new Vector3(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y, 0);

                    //dash sideways only
                    transform.position += new Vector3(rigid.velocity.normalized.x * 0.5f, 0, 0);

                    //dash cooltime
                    canDash = false;
                    StartCoroutine(dashCool());
                }
            }



        }
    }

    IEnumerator dashCool()
    {
        yield return new WaitForSeconds(5);
        canDash = true;
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject.Find("Weapon").GetComponentInChildren<BoxCollider2D>().enabled = false;
        GameObject.Find("Weapon").GetComponentInChildren<WeaponManager>().damage = 0f;
    }


    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1))
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
    }

}
