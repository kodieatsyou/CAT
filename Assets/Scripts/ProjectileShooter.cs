using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{

    public Sprite shooterSprite;
    public GameObject projectile;
    public float projectileSpeed;


    SpriteRenderer shooterSpriteRenderer;
    Vector3 shooterPosition;

    // Start is called before the first frame update
    void Start()
    {
        shooterSpriteRenderer = GetComponentInParent<SpriteRenderer>();
        shooterPosition = shooterSpriteRenderer.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            
        }
    }
}
