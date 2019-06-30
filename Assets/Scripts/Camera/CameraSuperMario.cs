using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSuperMario : MonoBehaviour, ResettableGameobject
{
    public GameObject player;
    public Transform leftCamBound;
    public Transform rightCamBound;
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
    private float startYPlayer;
    private float startYCamera;
    private bool moveX;
    private bool movingLeft;
    private bool movingRight;
    private bool crossedLeftBound;
    private bool crossedRightBound;
    private float playerXBeforeCrossingLeftBound;
    private float playerXBeforeCrossingRightBound;
    private Camera cam;
    private bool toggleMoveX;
    private bool resetted;

    private void Start()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        cam = gameObject.GetComponent<Camera>();
        cameraX = transform.position.x + offsetX;
        cameraY = transform.position.y + offsetY;
        cameraZ = transform.position.z;

        playerX = player.transform.position.x;
        playerY = player.transform.position.y;
        deltaX = Mathf.Abs(playerX - cameraX);
        deltaY = Mathf.Abs(playerY - cameraY);
        startYPlayer = playerY;
        startYCamera = transform.position.y;

        resetted = false;
        moveX = true;
        toggleMoveX = true;
        movingLeft = false;
        movingRight = false;
        crossedLeftBound = false;
        crossedRightBound = false;
        playerXBeforeCrossingLeftBound = 0f;
        playerXBeforeCrossingRightBound = 0f;
    }

    private void Update()
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

        CheckBoundsCrossed();

        if (moveX && toggleMoveX)
        {
            cameraX = playerX + offsetX;
        }

        if (marioStyleY)
        {
            if (playerY == startYPlayer)
            {
                cameraY = playerY + deltaY;
            }
            if (playerY < transform.position.y - deltaY)
            {
                cameraY = playerY + deltaY;
            }
            else if (playerY > transform.position.y + (deltaY / 3))
            {
                cameraY = playerY - (deltaY / 3);
            }
        }
        if(!marioStyleY || resetted)
        {
            cameraY = playerY + deltaY;
            resetted = false;
        }

        cameraY += offsetY;

        transform.position = new Vector3(cameraX, cameraY, cameraZ);
    }

    private void CheckBoundsCrossed()
    {
        // Screen reaches left border
        if (movingLeft && playerXBeforeCrossingLeftBound == 0 && cameraX <= leftCamBound.position.x + cam.aspect * cam.orthographicSize)
        {
            playerXBeforeCrossingLeftBound = playerX;
            moveX = false;
            crossedLeftBound = true;
        }
        // Screen reaches right border
        if (movingRight && playerXBeforeCrossingRightBound == 0 && cameraX >= rightCamBound.position.x - cam.aspect * cam.orthographicSize)
        {
            playerXBeforeCrossingRightBound = playerX;
            crossedRightBound = true;
            moveX = false;
        }
        // Camera should start following again if going left
        if (crossedRightBound &&!crossedLeftBound && movingLeft && playerX < playerXBeforeCrossingRightBound)
        {
            playerXBeforeCrossingRightBound = 0f;
            crossedRightBound = false;
            moveX = true;
        }
        // Camera should start following again if going left
        if (crossedLeftBound && !crossedRightBound && movingRight && playerX > playerXBeforeCrossingLeftBound)
        {
            playerXBeforeCrossingLeftBound = 0f;
            crossedLeftBound = false;
            moveX = true;
        }
    }

    public void toggleXMovement()
    {
        toggleMoveX = !toggleMoveX;
    }

    public void SetBounds(Transform leftCamBound, Transform rightCamBound)
    {
        if (leftCamBound != null)
        {
            this.leftCamBound = leftCamBound;
            playerXBeforeCrossingLeftBound = 0f;
            crossedLeftBound = false;
            moveX = true;
        }
        if (rightCamBound != null)
        {
            this.rightCamBound = rightCamBound;
            playerXBeforeCrossingRightBound = 0f;
            crossedRightBound = false;
            moveX = true;
        }
    }

    public void Reset()
    {
        resetted = true;
    }
}
