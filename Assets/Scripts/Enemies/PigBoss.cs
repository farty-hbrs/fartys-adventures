using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigBoss : EnemyFollow, ResettableGameobject
{
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject trigger;
    public Transform leftCamBound;
    public Transform rightCamBound;
    public GameObject pigBody;

    public Transform leftCamBoundBefore;
    public Transform rightCamBoundBefore;

    private bool facingLeft;
    private bool hittable;
    private LevelManager levelManager;
    private Rigidbody2D rbPlayer;
    private Rigidbody2D rbEnemy;
    private PlayerMovement pmPlayer;
    private CameraSuperMario camScript;

    private Vector2 startPos;
    private int selectedHitsToKill;

    void Start()
    {
        camScript = FindObjectOfType<CameraSuperMario>();
        target = GameObject.FindWithTag("Player");
        rbPlayer = target.GetComponent<Rigidbody2D>();
        rbEnemy = GetComponent<Rigidbody2D>();
        pmPlayer = target.GetComponent<PlayerMovement>();
        levelManager = FindObjectOfType<LevelManager>();
        facingLeft = true;
        hittable = true;
        startPos = new Vector2(transform.position.x, transform.position.y);
        selectedHitsToKill = hitsToKill;
    }

    void Update()
    {
        //transform.LookAt(player.transform);

        if (transform.position.x > target.transform.position.x && !facingLeft)
        {
            Flip();
        }
        if (transform.position.x < target.transform.position.x && facingLeft)
        {
            Flip();
        }

        if (Mathf.Abs(transform.position.x - target.transform.position.x) >= minDist && hittable)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.transform.position.x, transform.position.y), speed * Time.deltaTime);

            if (Mathf.Abs(transform.position.x - target.transform.position.x) <= maxDist)
            {
                // If the enemy can shoot or something like that you can implement it here
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && hittable)
        {
            Color color;
            ColorUtility.TryParseHtmlString("#FF8181", out color);
            pigBody.GetComponent<SpriteRenderer>().color = color;
            if (hitsToKill == 1)
            {
                camScript.SetBounds(leftCamBound, rightCamBound);
                //Destroy(leftWall);
                Destroy(rightWall);
                Destroy(trigger);
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
        }
    }

    IEnumerator FreezePlayer()
    {
        yield return new WaitForSeconds(1f);
        hittable = true;
        killCollider.enabled = true;
        rbEnemy.bodyType = RigidbodyType2D.Dynamic;
        Color color;
        ColorUtility.TryParseHtmlString("#FFFFFF", out color);
        pigBody.GetComponent<SpriteRenderer>().color = color;
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
        camScript.SetBounds(leftCamBoundBefore, rightCamBoundBefore);
        hitsToKill = selectedHitsToKill;
        transform.position = startPos;
    }
}
