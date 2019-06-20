using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienScript : MonoBehaviour { 


    private GameObject target = null;
    private Vector3 startPos;
    private Vector3 newPos;
    private Vector3 lastPos;


    // Start is called before the first frame update
    void Start()
    {
        target = null;
    }

    // Update is called once per frame
    void Update()
    {
        startPos = target.transform.position;
        lastPos = startPos;
        newPos = startPos;
        newPos.y += 0.25f;
        target.transform.position = newPos;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        target = col.gameObject;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        target = null;
    }
}
