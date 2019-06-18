using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetsHidden : MonoBehaviour
{
    public SpriteRenderer HideObject;
    public SpriteRenderer RevealObject;

    // Start is called before the first frame update
    

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HideObject.enabled = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        RevealObject.enabled = true;
    }
}
