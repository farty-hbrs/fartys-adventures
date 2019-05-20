using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSuperMario : MonoBehaviour
{
    public GameObject player;
    public Transform rightCamBoundary;
    public Transform levelEnd;
    public bool marioStyleY;
    public float offsetX;
    public float offsetY;

    private float deltaX;
    private float deltaY;
    private float cameraX;
    private float cameraY;
    private float cameraZ;
    private float playerX;
    private float playerY;
    private float lastX;
    private bool moveX;
    private bool movingLeft;
    private bool movingRight;
    private float playerXeforeCrossingRightBoundary;
    private Camera cam;

    private void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        moveX = true;
        movingLeft = false;
        movingRight = false;
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        cameraX = transform.position.x + offsetX;
        cameraY = transform.position.y + offsetY;
        cameraZ = transform.position.z;
        playerX = player.transform.position.x;
        playerY = player.transform.position.y;
        deltaX = Mathf.Abs(playerX - cameraX);
        deltaY = Mathf.Abs(playerY - cameraY);
        playerXeforeCrossingRightBoundary = 0f;
    }

    private void Update()
    {
        setCameraPosition();
    }

    private void LateUpdate()
    {
        setCameraPosition();
    }

    private void setCameraPosition()
    {
        playerX = player.transform.position.x;
        playerY = player.transform.position.y;

        movingLeft = playerX < lastX;
        movingRight = playerX > lastX;
  
        lastX = playerX;

        // Screen reaches right border
        if (movingRight && playerXeforeCrossingRightBoundary == 0 && transform.position.x >= rightCamBoundary.position.x - cam.aspect * cam.orthographicSize)
        {
            playerXeforeCrossingRightBoundary = playerX;
            moveX = false;
        }
        // Camera should start following again if going left
        if (movingLeft && playerX < playerXeforeCrossingRightBoundary)
        {
            playerXeforeCrossingRightBoundary = 0f;
            moveX = true;
        }

        if (moveX)
        {
            cameraX = playerX + offsetX;
        }

        if (marioStyleY)
        {
            yFollow();
        }
        else
        {
            cameraY = playerY + deltaY;
        }
        cameraY += offsetY;

        transform.position = new Vector3(cameraX, cameraY, cameraZ);
    }

    private void yFollow()
    {
        if(playerY < transform.position.y - deltaY)
        {
            cameraY = playerY + deltaY;
        }
        else if (playerY > transform.position.y + deltaY)
        {
            cameraY = playerY - deltaY;
        }
    }
}
