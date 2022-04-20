using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject killParticle;
    public bool alive;

    GameObject gc;

    PlayerMovement movementScript;

    // Start is called before the first frame update
    void Start()
    {
        alive = true;
        gc = GameObject.FindGameObjectWithTag("GameController");
        GetComponent<Rigidbody2D>().gravityScale = 3f;
        GetComponent<SpriteRenderer>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Kill()
    {
        if(alive)
        {
            Instantiate(killParticle, gameObject.transform);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().gravityScale = 0f;
            GetComponent<CircleCollider2D>().enabled = false;
            alive = false;
            gc.GetComponent<GameController>().Respawn();
        }
    }

    public void Spawn()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Rigidbody2D>().gravityScale = 3f;
        GetComponent<CircleCollider2D>().enabled = true;
        alive = true;
    }
}
