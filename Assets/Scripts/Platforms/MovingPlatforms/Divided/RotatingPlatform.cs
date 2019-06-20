using UnityEngine;
using System.Collections;

public class RotatingPlatform : MonoBehaviour
{

    public float speed = 3f;

    void Start()
    {

        

    }

    void Update()
    {
        transform.Rotate(0, 0, speed);
    }





    }
