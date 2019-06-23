using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleXMovement : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Camera cam = FindObjectOfType<Camera>();
            cam.GetComponent<CameraSuperMario>().toggleXMovement();
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
