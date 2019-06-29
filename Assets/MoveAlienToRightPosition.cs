using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlienToRightPosition : MonoBehaviour
{
    public GameObject target;

    private GameObject targetIntern;
    // Start is called before the first frame update
    void Start()
    {
        targetIntern = null;
    }

    // Update is called once per frame
    void Update()
    {
        targetIntern.transform.position = Vector2.MoveTowards(targetIntern.transform.position, new Vector2(583.7f, 122.5f), 20 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            targetIntern = target;
        }
    }
}
