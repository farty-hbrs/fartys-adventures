using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkewPlatform : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().SetMoveX(false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().SetMoveX(true);
        }
    }
}
