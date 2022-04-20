using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{

    public float bounceHeight = 5f;
    public bool canOnlyBounceOnce = false;
    public Sprite hasBouncedSprite;
    Sprite startSprite;
    bool hasAlreadyBounced = false;
    Vector2 bounceVector;

    // Start is called before the first frame update
    void Start()
    {
        bounceVector = new Vector2(0, bounceHeight);
        startSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        hasAlreadyBounced = false;
        gameObject.tag = "Untagged";
        EventManager.instance.ResetTraps += ResetTrap;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(canOnlyBounceOnce && !hasAlreadyBounced)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(bounceVector, ForceMode2D.Impulse);
                GetComponent<SpriteRenderer>().sprite = hasBouncedSprite;
                gameObject.tag = "Floor";
                hasAlreadyBounced = true;
            }
            if(!canOnlyBounceOnce)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(bounceVector, ForceMode2D.Impulse);
            }
            
        }
    }

    void ResetTrap()
    {
        hasAlreadyBounced = false;
        gameObject.tag = "Untagged";
        gameObject.GetComponent<SpriteRenderer>().sprite = startSprite;
    }
}
