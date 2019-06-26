using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectOnTrigger : MonoBehaviour, ResettableGameobject
{
    public GameObject objectToMove;
    public GameObject destination;
    public float speed = 5;

    private Vector2 startPos;

    private bool triggered = false;

    private void Start()
    {
        startPos = new Vector2(objectToMove.transform.position.x, objectToMove.transform.position.y);
    }

    void Update()
    {
        if (triggered)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, destination.transform.position, Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            triggered = true;
        }
    }

    public void Reset()
    {
        objectToMove.transform.position = startPos;
        triggered = false;
    }
}
