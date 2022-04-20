using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlatform : MonoBehaviour
{
    // Start is called before the first frame update

    BoxCollider2D[] colliders;

    void Start()
    {
        EventManager.instance.ResetTraps += ResetTrap;
        colliders = GetComponents<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetTrap()
    {
        this.GetComponent<SpriteRenderer>().color = Color.white;
        foreach (BoxCollider2D c in colliders)
        {
            c.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            this.GetComponent<SpriteRenderer>().color = Color.grey;
            foreach(BoxCollider2D c in colliders)
            {
                c.enabled = false;
            }
        }
    }
}
