using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiketrap : MonoBehaviour
{
    public GameObject Spike1, Spike2, Spike3;


    private void Start()
    {
        deactivateTraps();
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Invoke("activateTraps", 0.1f);
            Invoke("deactivateTraps", 0.2f);
        }
    }

    // Update is called once per frame
     void activateTraps()
    {
        
            Spike1.SetActive(true);
            Spike2.SetActive(true);
            Spike3.SetActive(true);

        
    }
    void deactivateTraps()
    {

        Spike1.SetActive(false);
        Spike2.SetActive(false);
        Spike3.SetActive(false);


    }
}
