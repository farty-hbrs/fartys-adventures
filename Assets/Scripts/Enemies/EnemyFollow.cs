using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 4;
    public float minDist = 10;
    public float maxDist = 1;

    private Transform player;
    private bool facingLeft;


    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        facingLeft = true;
    }

    void Update()
    {
        //transform.LookAt(player.transform);

        if (transform.position.x > player.position.x && !facingLeft)
        {
            Flip();
        }
        if (transform.position.x < player.position.x && facingLeft)
        {
            Flip();
        }

        if (Mathf.Abs(transform.position.x - player.position.x) >= minDist)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), speed * Time.deltaTime);
            //transform.position += transform.forward * MoveSpeed * Time.deltaTime;



            if (Mathf.Abs(transform.position.x - player.position.x) <= maxDist)
            {
                // If the enemy can shoot or something like that you can implement it here
            }

        }
    }

    void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
