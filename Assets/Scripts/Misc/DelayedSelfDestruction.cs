using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedSelfDestruction : MonoBehaviour
{
    public float Delay;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invoke("SelfDestroy", Delay);
    }

    void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
