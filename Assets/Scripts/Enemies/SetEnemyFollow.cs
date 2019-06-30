using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEnemyFollow : MonoBehaviour, ResettableGameobject
{
    public GameObject enemy;
    public bool enableEnemyFollow;
    private EnemyFollow ef;
    private bool switched = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(enemy != null)
        {
            ef = enemy.GetComponent<EnemyFollow>();
            if (collision.tag == "Player" && ef != null)
            {
                ef.enabled = enableEnemyFollow;
                switched = true;
            }
        }
    }

    public void Reset()
    {
        if (switched && ef != null)
        {
            ef.enabled = !enableEnemyFollow;
            switched = false;
        }
    }
}
