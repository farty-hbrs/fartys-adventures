using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectOnTrigger : MonoBehaviour
{
    public GameObject objectToMove;
    public GameObject destination;
    public float speed = 5;

    private bool triggered = false;

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
}
