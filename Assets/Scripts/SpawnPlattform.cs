using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlattform : MonoBehaviour
{
    public Transform[] Plattform;
    public GameObject obj;

    

    
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            for (int i = 0; i < Plattform.Length; i++)
            {
                Instantiate(obj, Plattform[i].position, Plattform[i].rotation);
            }

        }
    }

    // Update is called once per frame
    
    

    
}
