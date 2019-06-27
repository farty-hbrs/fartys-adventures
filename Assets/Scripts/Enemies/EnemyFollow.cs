using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour, ResettableGameobject
{
    public float speed = 4;
    public float minDist = 10;
    public float maxDist = 1;
    public Collider2D killCollider;

    public int hitsToKill = 1;
    public GameObject target;

    protected bool facingLeft;
    protected bool hittable;
    protected LevelManager levelManager;
    protected Rigidbody2D rbPlayer;
    protected Rigidbody2D rbEnemy;
    protected PlayerMovement pmPlayer;
    protected Animator anim;


    void Start()
    {
        target = GameObject.FindWithTag("Player");
        rbPlayer = target.GetComponent<Rigidbody2D>();
        rbEnemy = GetComponent<Rigidbody2D>();
        pmPlayer = target.GetComponent<PlayerMovement>();
        levelManager = FindObjectOfType<LevelManager>();
        anim = GetComponent<Animator>();
        facingLeft = true;
        hittable = true;
        anim.SetBool("isMoving", false);
    }

    void Update()
    {
        if (transform.position.x > target.transform.position.x && !facingLeft)
        {
            Flip();
        }
        if (transform.position.x < target.transform.position.x && facingLeft)
        {
            Flip();
        }

        if (speed > 0 && Mathf.Abs(transform.position.x - target.transform.position.x) >= minDist && hittable)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.transform.position.x, transform.position.y), speed * Time.deltaTime);
            anim.SetBool("isMoving", true);



            if (Mathf.Abs(transform.position.x - target.transform.position.x) <= maxDist)
            {
                // If the enemy can shoot or something like that you can implement it here
            }

        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && hittable)
        {
            if (hitsToKill == 1)
            {
                Destroy(gameObject);
            }
            if (rbPlayer.velocity.y <= 0)
            {
                rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, pmPlayer.jumpForce);
                hitsToKill--;
                hittable = false;
                rbEnemy.bodyType = RigidbodyType2D.Kinematic;
                killCollider.enabled = false;
                StartCoroutine(FreezePlayer());
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && hittable)
        {
            levelManager.RespawnPlayer();
            hittable = false;
            StartCoroutine(FreezePlayer());
        }
    }

    IEnumerator FreezePlayer()
    {
        yield return new WaitForSeconds(2f);
        hittable = true;
        killCollider.enabled = true;
        rbEnemy.bodyType = RigidbodyType2D.Dynamic;
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
        throw new System.NotImplementedException();
    }
}
