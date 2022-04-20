using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenTrap : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite hiddenSprite;
    Sprite trapSprite;

    void Start()
    {
        trapSprite = GetComponent<SpriteRenderer>().sprite;
        if (hiddenSprite == null)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        } else
        {
            GetComponent<SpriteRenderer>().sprite = hiddenSprite;
        }
        
        EventManager.instance.ResetTraps += ResetTrap;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetTrap()
    {
        if (hiddenSprite == null)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = hiddenSprite;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (hiddenSprite == null)
            {
                GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = trapSprite;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (hiddenSprite == null)
            {
                GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = trapSprite;
            }
        }
    }

    public void UnHide()
    {
        if (hiddenSprite == null)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = trapSprite;
        }
    }

}
