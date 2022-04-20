using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpike : MonoBehaviour
{

    public GameObject leftBound;
    public GameObject rightBound;
    public GameObject spike;
    public float extraTriggerRoom = 0.5f;
    BoxCollider2D trigger;
    public float spikeSpeed = 1f;
    bool tracking = false;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<BoxCollider2D>();
        SetTriggerBounds();
        tracking = false;
        EventManager.instance.ResetTraps += ResetTrap;
    }

    // Update is called once per frame
    void Update()
    {
        if(tracking)
        {
            Vector3 movePos;
            if(player.transform.position.x < gameObject.transform.position.x)
            {
                movePos = new Vector3(Mathf.Clamp(player.transform.position.x, leftBound.transform.position.x, gameObject.transform.position.x), spike.transform.position.y, spike.transform.position.z);
            } else if(player.transform.position.x >= gameObject.transform.position.x)
            {
                movePos = new Vector3(Mathf.Clamp(player.transform.position.x, gameObject.transform.position.x, rightBound.transform.position.x), spike.transform.position.y, spike.transform.position.z);
            } else
            {
                movePos = spike.transform.position;
            }
            
            var step = spikeSpeed * Time.deltaTime;
            spike.transform.position = Vector3.MoveTowards(spike.transform.position, movePos, step);
        }
    }

    void SetTriggerBounds()
    {
        //rightBound.transform.localPosition = new Vector3(Mathf.Round(rightBound.transform.localPosition.x), rightBound.transform.localPosition.y, rightBound.transform.localPosition.z);
        //leftBound.transform.localPosition = new Vector3(Mathf.Round(leftBound.transform.localPosition.x), leftBound.transform.localPosition.y, leftBound.transform.localPosition.z);
        trigger.size = new Vector2((rightBound.transform.localPosition.x * 2 + 1) + extraTriggerRoom, trigger.size.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            tracking = true;
            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            tracking = false;
            player = null;
        }
    }

    void ResetTrap()
    {
        spike.transform.localPosition = Vector3.zero;
    }
}
