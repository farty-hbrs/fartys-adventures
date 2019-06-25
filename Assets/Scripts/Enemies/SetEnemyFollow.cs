using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEnemyFollow : MonoBehaviour
{
    public GameObject enemy;
    public bool enableEnemyFollow;
    private EnemyFollow ef;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ef = enemy.GetComponent<EnemyFollow>();
        if (collision.tag == "Player" && ef != null)
        {
            ef.enabled = enableEnemyFollow;
        }
    }
}
