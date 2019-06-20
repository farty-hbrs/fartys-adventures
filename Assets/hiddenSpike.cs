using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hiddenSpike : MonoBehaviour
{
    private LevelManager levelmanger;
    public float speed;
    public float distance;

    private Vector3 startPos;
    private Vector3 newPos;
    private Vector3 lastPos;
    private SpriteRenderer spriteRenderer;

    private GameObject target = null;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        levelmanger = FindObjectOfType<LevelManager>();
        target = null;
        startPos = transform.position;
        lastPos = startPos;
        
    }

    // Update is called once per frame
    void Update()
    {
        GenerateNewPosition();
        transform.position = newPos;
    }
     
    void GenerateNewPosition()
    {
        newPos = startPos;
        newPos.y += Mathf.PingPong(Time.time * speed, distance) - distance / 2;
    }
     
    
    
}
