using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{

    bool disableBox = false;

    // Start is called before the first frame update
    void Start()
    {
        disableBox = false;
        EventManager.instance.ResetTraps += ResetTrap;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!disableBox)
            {
                collision.gameObject.GetComponent<PlayerController>().Kill();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(!disableBox)
            {
                collision.gameObject.GetComponent<PlayerController>().Kill();
            }  
        }
    }

    public void DisableKillBox()
    {
        disableBox = true;
    }

    void ResetTrap()
    {
        disableBox = false;
    }
}
