using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienScript : MonoBehaviour { 

    public GameObject alien;
    public Rigidbody2D body;

    private GameObject target;
    private Vector3 startPos;
    private Vector3 newPos;
    private Vector3 lastPos;


    // Start is called before the first frame update
    void Start()
    {
        target = null;
    }

    // Update is called once per frame
    void Update()
    {
        target.transform.position = Vector2.MoveTowards(target.transform.position, new Vector2(target.transform.position.x, alien.transform.position.y), 3 * Time.deltaTime);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            target = col.gameObject;
            body.mass = 0;
            body.gravityScale = 0;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        body.mass = 1;
        body.gravityScale = 5;
        target = null;
    }
}
