using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedSelfDestruction : MonoBehaviour
{
    public float Delay;
    private Vector2 startPos;


    private void Start()
    {
       startPos = new Vector2(transform.position.x, transform.position.y);
    }


    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invoke("SelfDestroy", Delay);
    }

    void SelfDestroy()
    {
        gameObject.SetActive(false);
    }


    public void Reset()
    {

        if (!gameObject.activeSelf)
        {
            transform.position = startPos;
            gameObject.SetActive(true);
        }
    }
}
