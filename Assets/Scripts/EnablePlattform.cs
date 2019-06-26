﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePlattform : MonoBehaviour
{
    public GameObject[] Plattform;


    private void Start()
    {
        deactivate();
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            for (int i = 0; i < Plattform.Length; i++)
            {
                Plattform[i].SetActive(true);
            }

        }
    }

    // Update is called once per frame
    
    void deactivate()
    {
        for (int i = 0; i < Plattform.Length; i++)
        {
            Plattform[i].SetActive(false);
        }

    }
}
