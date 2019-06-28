using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBoss : EnemyFollow, ResettableGameobject
{
    public GameObject killSwitch;
    public Sprite killSwitchActiveSprite;
    public Sprite killSwitchInactiveSprite;
    public GameObject rockWrapper;
    public GameObject trigger;
    public GameObject rightWall;
    public GameObject[] rats;
    public GameObject[] ratTriggers;
    private bool touchedSwitch;
    private int currentRat;

    void Start()
    {
        target = killSwitch;
        rbPlayer = target.GetComponent<Rigidbody2D>();
        rbEnemy = GetComponent<Rigidbody2D>();
        pmPlayer = target.GetComponent<PlayerMovement>();
        levelManager = FindObjectOfType<LevelManager>();
        anim = GetComponent<Animator>();
        facingLeft = true;
        hittable = true;
        touchedSwitch = false;
        anim.SetBool("isMoving", false);
        startPos = new Vector2(transform.position.x, transform.position.y);
        selectedHitsToKill = hitsToKill;
        currentRat = 0;
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

        if (!touchedSwitch && speed > 0 && Mathf.Abs(transform.position.x - target.transform.position.x) >= minDist && hittable)
        {
            if (rbEnemy != null)
            {
                rbEnemy.velocity = new Vector2(speed * (facingLeft ? -1 : 1), rbEnemy.velocity.y);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.transform.position.x, transform.position.y), speed * Time.deltaTime);
            }
            anim.SetBool("isMoving", true);
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
                Destroy(rockWrapper);
                Destroy(trigger);
                Destroy(rightWall);
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
        if (collision.gameObject == killSwitch)
        {
            touchedSwitch = true;
            killSwitch.GetComponent<SpriteRenderer>().sprite = killSwitchActiveSprite;
            foreach (Transform child in rockWrapper.transform)
            {
                if (child != rockWrapper.transform.GetChild(0))
                {
                    child.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                }
            }
        }
        if (currentRat < rats.Length)
        {
            if (collision.gameObject == ratTriggers[currentRat])
            {
                ratTriggers[currentRat].SetActive(false);
                rats[currentRat].SetActive(true);
                currentRat++;
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

    public new void Reset()
    {
        foreach (Transform child in rockWrapper.transform)
        {
            if (child != rockWrapper.transform.GetChild(0))
            {
                child.gameObject.GetComponent<KillSwitchRock>().Reset();
            }
        }
        foreach(GameObject obj in rats)
        {
            obj.SetActive(false);
        }
        foreach(GameObject obj in ratTriggers)
        {
            obj.SetActive(true);
        }
        killSwitch.GetComponent<SpriteRenderer>().sprite = killSwitchInactiveSprite;
        hitsToKill = selectedHitsToKill;
        transform.position = startPos;
        touchedSwitch = false;
        currentRat = 0;
    }
}
