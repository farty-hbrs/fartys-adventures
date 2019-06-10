using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    public float circleRadius = 0.1f;
    public float jumpForce = 3;
    public Transform feetPos;
    public LayerMask whatIsGround;


    private Rigidbody2D rb;
    private Animator anim;
    private float moveInput;
    private bool isGrounded;
    private bool isJumping;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        moveInput = CrossPlatformInputManager.GetAxis("Horizontal");
        anim.SetFloat("speed", Mathf.Abs(moveInput));
        isGrounded = Physics2D.OverlapCircle(feetPos.position, circleRadius, whatIsGround);
        anim.SetBool("isGrounded", isGrounded);

        rb.velocity = new Vector2(speed * moveInput, rb.velocity.y);

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
        if (isGrounded && CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            
            anim.SetTrigger("takeOff");
            isJumping = true;
            rb.velocity = Vector2.up * jumpForce;
        }
        else if (isGrounded) {
            isJumping = false;
        }
        anim.SetBool("isJumping", isJumping);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
