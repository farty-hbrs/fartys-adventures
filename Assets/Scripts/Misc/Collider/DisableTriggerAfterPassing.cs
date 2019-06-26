using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTriggerAfterPassing : MonoBehaviour, ResettableGameobject
{
    public Collider2D triggerCollider;
    private Collider2D ownCollider;
    private GameObject player;
    public bool passedTrigger;
    
    void Start()
    {
        ownCollider = GetComponent<Collider2D>();
        if (triggerCollider == null)
        {
            triggerCollider = ownCollider;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !passedTrigger)
        {
            triggerCollider.GetComponent<DisableTriggerAfterPassing>().passedTrigger = true;
        }
    }

    void Update()
    {
        if(triggerCollider.GetComponent<DisableTriggerAfterPassing>().passedTrigger && triggerCollider.GetComponent<DisableTriggerAfterPassing>().player.transform.position.x > triggerCollider.transform.position.x)
        {
            ownCollider.isTrigger = false;
        }
    }

    public void Reset()
    {
        if (passedTrigger && player.transform.position.x < transform.position.x)
        {
            passedTrigger = false;
            ownCollider.isTrigger = true;
        }
    }
}
