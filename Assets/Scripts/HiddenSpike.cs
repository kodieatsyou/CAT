using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenSpike : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GetComponent<SpriteRenderer>().enabled = true;
            collision.transform.position = gameObject.transform.position;
            collision.GetComponent<PlayerController>().Kill();
        }
    }
}
