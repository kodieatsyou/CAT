using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject SpawnParticle;

    Animator anim;
    bool activated = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        activated = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActivateThisCheckpoint()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().SetCheckPoint(this.gameObject);
        activated = true;
        anim.Play("Base Layer.Checkpoint_Activate", 0, 0.25f);
        anim.StopPlayback();
    }

    void PlayParticle()
    {
        Instantiate(SpawnParticle, gameObject.transform);
    }

    public void DeActivateCheckPoint()
    {
        activated = false;
        anim.Play("Base Layer.Checkpoint_UnActivated", 0, 0.25f);
        anim.StopPlayback();
    }

    public void PlaySpawnAnimation()
    {
        anim.Play("Base Layer.Checkpoint_Respawn", 0, 0.25f);
        anim.StopPlayback();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!activated)
            {
                ActivateThisCheckpoint();
            }
        }
    }
}
