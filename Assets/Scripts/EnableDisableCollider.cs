using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public BoxCollider2D collider;

    // Update is called once per frame
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
            Invoke("reactivate", 0.3f);
        }
    }


    void reactivate()
    {
        collider.enabled = true;
    }
}
