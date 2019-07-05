﻿using UnityEngine;
using System.Collections;

public class RotatingPlatform : MonoBehaviour
{
    public float speed = 3f;

    void Update()
    {
        transform.Rotate(0, 0, speed);
    }
}
