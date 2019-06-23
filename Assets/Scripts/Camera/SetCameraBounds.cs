using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraBounds : MonoBehaviour
{
    public Transform leftCamBound;
    public Transform rightCamBound;
    public bool toggle;
    private CameraSuperMario cam;
    private Transform leftCamBoundBefore;
    private Transform rightCamBoundBefore;

    private void Start()
    {
        cam = FindObjectOfType<CameraSuperMario>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            cam.SetBounds(leftCamBound, rightCamBound);
        }
    }
}
