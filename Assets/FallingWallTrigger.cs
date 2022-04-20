using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingWallTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponentInParent<FallingWall>().Trigger();
        }
    }

    public void SizeCollider(int height)
    {
        GetComponent<BoxCollider2D>().size = new Vector2(height, 1);
        GetComponent<BoxCollider2D>().offset = new Vector2(-(height/2.0f) - 0.5f, 0);
    }
}
