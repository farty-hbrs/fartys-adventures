using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportTo;
    public Sprite activeSprite;
    private Sprite inactiveSprite;
    private GameObject player;
    private bool jumped;

    private void Start()
    {
        inactiveSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            jumped = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = activeSprite;
            player = collision.gameObject;
            StartCoroutine(TeleportPlayer());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            jumped = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = inactiveSprite;
        }
    }

    IEnumerator TeleportPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        if (!jumped)
        {
            player.transform.position = teleportTo.position;
        }
       
    }
}
