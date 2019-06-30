using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownWhenPlayerJumpsOn : MonoBehaviour, ResettableGameobject
{
    public float delay = 0f;
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            StartCoroutine(Delay());
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
            rb.bodyType = RigidbodyType2D.Static;
            fellDown = false;
            transform.position = startPos;
        }
    }
}
