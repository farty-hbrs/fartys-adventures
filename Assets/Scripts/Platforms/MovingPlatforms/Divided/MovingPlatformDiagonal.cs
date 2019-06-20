using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformDiagonal : MonoBehaviour
{
    public enum Direction
    {
        RightUp,
        RightDown,
        LeftUp,
        LeftDown,
       
    }
    public Direction direction;

    public float speed;
    public float distance;
    public bool noReturn;

    private Vector3 startPos;
    private Vector3 newPos;
    private Vector3 lastPos;
    private SpriteRenderer spriteRenderer;

    private GameObject target = null;
    private Vector3 offset;

    void Start()
    {
        target = null;
        startPos = transform.position;
        lastPos = startPos;
    }

    void Update()
    {
        GenerateNewPosition();
        transform.position = newPos;
    }

    void GenerateNewPosition()
    {
        if (!noReturn) {
            newPos = startPos;
        }
        switch (direction)
        {
            case Direction.RightUp:
                newPos.y += Mathf.PingPong(Time.time * speed, distance) - distance / 2;
                newPos.x += Mathf.PingPong(Time.time * speed, distance) - distance / 2;
                break;
            case Direction.LeftDown:
                newPos.y -= Mathf.PingPong(Time.time * speed, distance) - distance / 2;
                newPos.x -= Mathf.PingPong(Time.time * speed, distance) - distance / 2;
                break;
            case Direction.RightDown:
                newPos.x += Mathf.PingPong(Time.time * speed, distance) - distance / 2;
                newPos.y -= Mathf.PingPong(Time.time * speed, distance) - distance / 2;
                break;
            case Direction.LeftUp:
                newPos.x -= Mathf.PingPong(Time.time * speed, distance) - distance / 2;
                newPos.y += Mathf.PingPong(Time.time * speed, distance) - distance / 2;
                break;
            
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        target = col.gameObject;
        offset = target.transform.position - transform.position;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        target = null;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            target.transform.position = transform.position + offset;
        }
    }
}
