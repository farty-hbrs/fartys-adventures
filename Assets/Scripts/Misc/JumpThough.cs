using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpThough : MonoBehaviour
{
    private BoxCollider2D box;

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        box.enabled = false;
    }

    // Update is called once per frame
    private void OnTriggerExit2D(Collider2D collision)
    {
        Invoke("enable", 0.1f);
    }

    private void enable()
    {
        box.enabled = true;
    }
}
