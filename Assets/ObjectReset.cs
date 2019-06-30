using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReset : MonoBehaviour
{
    // Start is called before the first frame update
    

    // Update is called once per frame
     void Reset()
    {
        gameObject.SetActive(false) ;
    }
}
