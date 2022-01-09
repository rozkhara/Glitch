using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    public GM GameManager;
    public float maxSpeed;
    public float jumpPower;
    bool canDash = true;
    bool wasHit = false;
    public bool rightWallBoost = false;
    public bool leftWallBoost = false;
    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer SpriteRenderer;

    public float health;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GM>();

        animator.SetBool("isCharging", false);
        animator.SetBool("isMoving", false);
        animator.SetBool("isGrounded", true);
    }

    void Start()
    {
        rightWallBoost = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isOnPause)
        {
            //jump
            if (Input.GetButtonDown("Jump"))
            {
                //disable double jump
                if (animator.GetBool("isGrounded") && !(animator.GetBool("isCharging")))
                {
                    //jump physics
                    rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

                    //set isGrounded boolean to false when player jumps
                    animator.SetBool("isGrounded", false);
                }
            }

            //move break
            if (Input.GetButtonUp("Horizontal"))
            {
                rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
            }

            //turn head
            if (!animator.GetBool("isCharging"))
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    if (leftWallBoost == true)
                    {
                        Debug.Log("leftwallboosttrue slow");
                        maxSpeed = 2;
                        bool leftWallBoost = false;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    if (rightWallBoost == true)
                    {
                        Debug.Log("rightwallboosttrue slow");
                        maxSpeed = 2;
                        bool rightWallBoost = false;
                    }
                }
            }





            //attack
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (!animator.GetBool("isCharging"))
                {
                    //immobile while charging attack
                    maxSpeed = 0;
                    animator.SetBool("isCharging", true);
                }
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
                animator.SetBool("isCharging", false);
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



            // if (transform.position.y != 0)
            // {

            //     animator.SetBool("isGrounded", false);
            // }

            AnimationUpdate();

        }
    }

    void AnimationUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }

    }

    IEnumerator dashCool()
    {
        yield return new WaitForSeconds(1);
        canDash = true;
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject.Find("Weapon").GetComponentInChildren<BoxCollider2D>().enabled = false;
        GameObject.Find("Weapon").GetComponentInChildren<WeaponManager>().damage = 0f;
    }

    IEnumerator invincible()
    {
        yield return new WaitForSeconds(2);
        wasHit = false;
    }

    IEnumerator wallBoostSlow()
    {
        yield return new WaitForSeconds(2);
        maxSpeed = 2;
        bool leftWallBoost = false;
        bool rightWallBoost = false;
        Debug.Log("wallBoostSlow");
    }



    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1))
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);



        // if (Input.GetKey(KeyCode.LeftArrow))
        // {
        //     if (leftWallBoost = true)
        //     {
        //         Debug.Log("leftwallboosttrue slow");
        //         maxSpeed = 2;
        //         bool leftWallBoost = false;
        //     }
        // }



    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            animator.SetBool("isGrounded", true);
        }
        if (collision.transform.CompareTag("Monster"))
        {
            // Physics2D.IgnoreCollsion(M)
            if (wasHit == false)
            {
                health -= 33;
                Debug.Log("Player Health : " + health);
                if (health <= 0)
                {
                    Destroy(gameObject);
                }
                wasHit = true;
                StartCoroutine(invincible());
            }
        }


    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                maxSpeed += 1;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                maxSpeed += 1;
            }

        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.transform.CompareTag("Wall"))
        {
            Debug.Log("Wall Exit");

            if (Input.GetKey(KeyCode.RightArrow))
            {
                leftWallBoost = true;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rightWallBoost = true;
            }

            StartCoroutine(wallBoostSlow());

        }

    }


}
