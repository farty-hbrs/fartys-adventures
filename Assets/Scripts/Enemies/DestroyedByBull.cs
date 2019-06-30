using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedByBull : MonoBehaviour, ResettableGameobject
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bull")
        {
            gameObject.SetActive(false);
        }
    }

    public void Reset()
    {
        gameObject.SetActive(true);
    }

}
