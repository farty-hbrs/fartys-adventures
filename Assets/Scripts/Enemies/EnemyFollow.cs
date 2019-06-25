using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 4;
    public float minDist = 10;
    public float maxDist = 1;
    public Collider2D killCollider;

    public float hitsToKill = 1;
    
    private GameObject player;
    private bool facingLeft;
    private bool hittable;
    private LevelManager levelManager;
    private Rigidbody2D rbPlayer;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rbPlayer = player.GetComponent<Rigidbody2D>();
        levelManager = FindObjectOfType<LevelManager>();
        facingLeft = true;
        hittable = true;
    }

    void Update()
    {
        //transform.LookAt(player.transform);

        if (transform.position.x > player.transform.position.x && !facingLeft)
        {
            Flip();
        }
        if (transform.position.x < player.transform.position.x && facingLeft)
        {
            Flip();
        }

        if (Mathf.Abs(transform.position.x - player.transform.position.x) >= minDist)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), speed * Time.deltaTime);
            //transform.position += transform.forward * MoveSpeed * Time.deltaTime;



            if (Mathf.Abs(transform.position.x - player.transform.position.x) <= maxDist)
            {
                // If the enemy can shoot or something like that you can implement it here
            }

        }
    }

    IEnumerator FreezePlayer()
    {
        yield return new WaitForSeconds(2f);
        hittable = true;
        killCollider.enabled = false;
    }

    void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
