using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityProjectileSpawner : MonoBehaviour
{

    public GameObject projectile;
    public GameObject spawner;
    float spawnHeight;
    bool readyToSpawnAnother = false;

    // Start is called before the first frame update
    void Start()
    {
        if(projectile != null)
        {
            spawnHeight = projectile.transform.localScale.y + 1f;
        }
        readyToSpawnAnother = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCanSpawn()
    {
        readyToSpawnAnother = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && readyToSpawnAnother == true)
        {
            readyToSpawnAnother = false;
            GameObject spawned = Instantiate(projectile, new Vector3(this.transform.position.x, this.transform.position.y + spawnHeight, this.transform.position.z), Quaternion.identity);
            if (spawned.GetComponent<GravityProjectile>() != null)
            {
                spawned.GetComponent<GravityProjectile>().setSpawner(spawner) ;
            } else
            {
                Destroy(spawned);
                Debug.Log("Wrong projectile spawned from: " + this.gameObject.name);
            }
            
        }
    }
}
