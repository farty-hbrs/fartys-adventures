using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSwitchRock : MonoBehaviour, ResettableGameobject
{
    private Vector2 startPos;

    void Start()
    {
        startPos = new Vector2(transform.position.x, transform.position.y);
    }

    public void Reset()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        transform.position = startPos;
    }
}
