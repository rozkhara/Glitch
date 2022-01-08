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
    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer SpriteRenderer;

    public float health;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        animator.SetBool("isCharging", false);
        animator.SetBool("isMoving", false);
        animator.SetBool("isGrounded", true);
    }

    void Start()
    {

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
                if (animator.GetBool("isGrounded"))
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

            //move
            if (Input.GetButtonDown("Horizontal"))
            {
                if (!animator.GetBool("isCharging"))
                    SpriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
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


    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1))
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
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
                if (health <= 0)
                {
                    Destroy(gameObject);
                }
                wasHit = true;
                StartCoroutine(invincible());
            }
        }

    }


}
