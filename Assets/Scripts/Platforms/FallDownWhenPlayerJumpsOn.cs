using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownWhenPlayerJumpsOn : MonoBehaviour, ResettableGameobject
{
    public float delay = 0f;
    private bool playerJumpedOn;
    private bool fellDown;

    private Vector2 startPos;
    private Rigidbody2D rb;

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        rb.bodyType = RigidbodyType2D.Static;
        fellDown = false;
        playerJumpedOn = false;
        
        if(startPos == null)
        {
            startPos = new Vector2(transform.position.x, transform.position.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerJumpedOn && !fellDown)
        {
            StartCoroutine(Delay());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerJumpedOn = true;
        }
    }

    IEnumerator Delay()
    {
        if (delay > 0f)
        { 
            yield return new WaitForSeconds(delay);
        }
        rb.bodyType = RigidbodyType2D.Dynamic;
        fellDown = true;
    }

    public void Reset()
    {
        if (fellDown)
        {
            transform.position = startPos;
            rb.bodyType = RigidbodyType2D.Static;
            fellDown = false;
            playerJumpedOn = false;
            transform.position = startPos;
        }
    }
}
