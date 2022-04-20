using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityProjectile : MonoBehaviour
{
    Rigidbody2D rb2d;
    public Vector2 speed;
    GameObject parentSpawner;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSpawner(GameObject spawner)
    {
        parentSpawner = spawner;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == parentSpawner)
        {
            Destroy(this.gameObject);
            parentSpawner.GetComponentInChildren<GravityProjectileSpawner>().setCanSpawn();
            //Debug.Log("Killed projectile");
        }
    }
}
