using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    public float circleRadius = 0.1f;
    public float jumpForce = 3;
    public Transform feetPosTopLeft;
    public Transform feetPosBottomRight;
    public LayerMask whatIsGround;
    public GameObject levelEnd;
    public GameObject nextLevelTrigger;


    private Rigidbody2D rb;
    private Animator anim;
    private float moveInput;
    private bool isGrounded;
    private bool isJumping;
    private bool pressedJump;
    private bool moveX;
    private bool reachedLevelEnd;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pressedJump = false;
        isJumping = false;
        moveX = true;
        reachedLevelEnd = false;
    }

    void FixedUpdate()
    {
        if (reachedLevelEnd)
        {
            if(transform.position.x < nextLevelTrigger.transform.position.x && moveX)
            {
                moveInput = 1;
            }
            else
            {
                moveInput = 0;
            }
        }
        else
        {
            moveInput = CrossPlatformInputManager.GetAxis("Horizontal");
        }
        anim.SetFloat("speed", Mathf.Abs(moveInput));
        isGrounded = Physics2D.OverlapArea(feetPosTopLeft.position, feetPosBottomRight.position, whatIsGround);
        anim.SetBool("isGrounded", isGrounded);

        if (moveX)
        {
            rb.velocity = new Vector2(speed * moveInput, rb.velocity.y);
        }

        // Side movement
        if (moveInput > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        // Jump
        if (isGrounded && pressedJump && !reachedLevelEnd)
        {
            pressedJump = false;
            anim.SetTrigger("takeOff");
            isJumping = true;
            rb.velocity = Vector2.up * jumpForce;
        }
        else if (isJumping && isGrounded) {
            isJumping = false;
        }
        anim.SetBool("isJumping", isJumping);
    }
    // Update is called once per frame
    void Update()
    {
        if(CrossPlatformInputManager.GetButtonDown("Jump") && isGrounded && !isJumping)
        {
            pressedJump = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == levelEnd)
        {
            Destroy(levelEnd);
            rb.velocity = new Vector2(0, rb.velocity.y);
            moveX = false;
            reachedLevelEnd = true;
            anim.SetTrigger("LevelEnd");
            StartCoroutine(FreezePlayer());
        }
    }

    IEnumerator FreezePlayer()
    {
        yield return new WaitForSeconds(1.5f);
        moveX = true;

    }

    public void SetMoveX(bool moveX)
    {
        this.moveX = moveX;
    }
}
