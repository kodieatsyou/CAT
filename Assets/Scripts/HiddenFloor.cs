using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenFloor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        EventManager.instance.ResetTraps += ResetTrap;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetTrap() 
    {
        GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
    }
}
