using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 0;

    private Vector3 startPos;
    private Vector3 newPos;
    private Vector3 lastPos;
    private SpriteRenderer spriteRenderer;
    private bool hittable;
    private LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();

        startPos = transform.position;
        lastPos = startPos;
        hittable = true;
        
        if(speed < 0)
        {
            speed = Random.Range(3f, 10f);
        }
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        newPos = startPos;
        newPos.x = newPos.x + Mathf.PingPong(Time.time * speed, 6) - 3;
        transform.position = newPos;

        if(newPos.x > lastPos.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }

        lastPos = newPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && hittable)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && hittable)
        {
            levelManager.RespawnPlayer();
            hittable = false;
            StartCoroutine(FreezePlayer());
        }
    }

    IEnumerator FreezePlayer()
    {
        yield return new WaitForSeconds(2f);
        hittable =  true;
    }
}
