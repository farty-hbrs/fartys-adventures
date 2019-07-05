using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableCollider : MonoBehaviour
{
    public new BoxCollider2D collider;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collider.enabled = false;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Invoke("reactivate", 1f);
        }
    }


    void reactivate()
    {
        collider.enabled = true;
    }
}
