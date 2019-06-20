using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownWhenPlayerJumpsOn : MonoBehaviour
{
    public float delay = 0f;
    private bool playerJumpedOn = false;
    private bool fellDown = false;

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
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        fellDown = true;
    }
}
