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
    private float m_LastTargetPosition;

    private void Start()
    {
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
        //float xMoveDelta = player.transform.position.x - m_LastTargetPosition;
        //cameraX = player.transform.position.x + deltaX + offsetX;
        playerX = player.transform.position.x;
        playerY = player.transform.position.y;

        cameraX = playerX + offsetX;

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
