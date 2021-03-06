﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ResettableGameobject
{
    public float speed = 0;
    public float distance = 6;

    private Vector3 startPos;
    private Vector3 newPos;
    private Vector3 lastPos;
    private SpriteRenderer spriteRenderer;
    private bool hittable;
    private LevelManager levelManager;
    private bool facingLeft;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();

        startPos = transform.position;
        lastPos = startPos;
        hittable = true;
        facingLeft = true;

        if (speed < 0)
        {
            speed = Random.Range(3f, 10f);
        }
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        anim = GetComponent<Animator>();
        if(anim != null && speed > 0f)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        newPos = startPos;
        newPos.x += Mathf.PingPong(Time.time * speed, distance) - distance / 2;
        transform.position = newPos;

        if (newPos.x > lastPos.x && facingLeft)
        {
            Flip();
        }
        if (newPos.x < lastPos.x && !facingLeft)
        {
            Flip();
        }
        lastPos = newPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && hittable)
        {
            gameObject.SetActive(false);
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

    void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void Reset()
    {
        transform.position = startPos;
        gameObject.SetActive(true);
        hittable = true;
    }
}
