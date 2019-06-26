using UnityEngine;

public class Player : MonoBehaviour
{
    private LevelManager levelManager;
    
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Checkpoint")
        {
            Destroy(collision.gameObject);
        }
    }
}
