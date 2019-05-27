using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    public float parallaxEffect;

    private Camera cam;
    private float length;
    private float startPos;
    
    void Start()
    {
        cam = FindObjectOfType<Camera>();
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    
    void Update()
    {
        float tmp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
    }
}
