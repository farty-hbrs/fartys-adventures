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
    public Transform HeadPosTopLeft;
    public Transform HeadPosBottomRight;
    public Transform FartPos;
    public GameObject fartCloud;
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
    private bool mayFartRandomly;

    private AudioManager am;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        am = FindObjectOfType<AudioManager>();
        pressedJump = false;
        isJumping = false;
        moveX = true;
        reachedLevelEnd = false;
        mayFartRandomly = true;
    }

    void FixedUpdate()
    {
        if (mayFartRandomly && (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") || anim.GetCurrentAnimatorStateInfo(0).IsName("Walk")))
        {
            StartCoroutine(RandomFart());
        }
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

        // Side movement sprite flipping
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
            StartCoroutine(Fart());
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

    IEnumerator RandomFart()
    {
        mayFartRandomly = false;
        int rand = Random.Range(1, 1000);
        if (rand < 100)
        {
            StartCoroutine(Fart());
            yield return new WaitForSeconds(Random.Range(4, 10));
        }
        mayFartRandomly = true;
    }

    IEnumerator Fart()
    {
        // Instantiate fart object with random rotation
        GameObject fart = Instantiate(fartCloud, FartPos.position, FartPos.rotation);
        Vector3 euler = fart.transform.eulerAngles;
        euler.z = Random.Range(0f, 360f);
        fart.transform.eulerAngles = euler;

        // Play random fart sound
        string soundName = "fart" + Random.Range(1, 5);
        am.Play(soundName);
        yield return new WaitForSeconds(2f);
        Destroy(fart);

    }

    public void SetMoveX(bool moveX)
    {
        this.moveX = moveX;
    }
}
