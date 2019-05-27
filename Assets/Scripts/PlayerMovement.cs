using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    public bool marioStyleJump;
    public float speed = 10;
    public float circleRadius = 3;
    public float jumpForce = 3;
    public float jumpTime = 3;
    public Transform feetPos;
    public LayerMask whatIsGround;


    private Rigidbody2D rb;
    private float moveInput;
    private float jumpTimeCounter;
    private bool isGrounded;
    private bool isJumping;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        moveInput = CrossPlatformInputManager.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * moveInput, rb.velocity.y);
    }
    // Update is called once per frame
    void Update()
    {
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
        isGrounded = Physics2D.OverlapCircle(feetPos.position, circleRadius, whatIsGround);
        if (isGrounded && CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }
        if (marioStyleJump) {
            if (isJumping && CrossPlatformInputManager.GetButton("Jump"))
            {
                if (jumpTimeCounter > 0)
                {
                    rb.velocity = Vector2.up * jumpForce;
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }
        }
        if (CrossPlatformInputManager.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }
}
