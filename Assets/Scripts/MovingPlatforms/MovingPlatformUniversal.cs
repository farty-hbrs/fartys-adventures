using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformUniversal : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left,
        Rotate
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
            case Direction.Up:
                newPos.y += Mathf.PingPong(Time.time * speed, distance) - distance / 2;
                break;
            case Direction.Down:
                newPos.y -= Mathf.PingPong(Time.time * speed, distance) - distance / 2;
                break;
            case Direction.Right:
                newPos.x += Mathf.PingPong(Time.time * speed, distance) - distance / 2;
                break;
            case Direction.Left:
                newPos.x -= Mathf.PingPong(Time.time * speed, distance) - distance / 2;
                break;
            case Direction.Rotate:
                transform.Rotate(0, 0, speed);
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
