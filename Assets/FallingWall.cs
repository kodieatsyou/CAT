using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingWall : MonoBehaviour
{

    List<GameObject> traps;
    BoxCollider2D trapCollider;
    public int height;
    public int width;

    public Sprite wallSprite;

    float curRotZ;
    bool falling = false;

    Vector3 fallRotation = new Vector3(0, 0, 90);
    private float convertedTime = 200;
    public float fallSpeed;
    private float smooth;

    public bool spikesHidden = true;

    // Start is called before the first frame update
    void Start()
    {
        traps = new List<GameObject>();
        trapCollider = GetComponent<BoxCollider2D>();
        EventManager.instance.ResetTraps += ResetTrap;
        this.GetComponent<SpriteRenderer>().sprite = wallSprite;
        curRotZ = transform.rotation.z;
        SetCollider();
        GenerateWall();
    }

    // Update is called once per frame
    void Update()
    {
        if(falling && curRotZ < 90)
        {
            smooth = Time.deltaTime * fallSpeed * convertedTime;
            transform.RotateAround(transform.position, fallRotation, smooth);
            curRotZ = transform.localRotation.eulerAngles.z;
        } else if(falling && curRotZ >= 90)
        {
            transform.eulerAngles = fallRotation;
            foreach(GameObject t in traps)
            {
                t.GetComponent<KillBox>().DisableKillBox();
            }
            falling = false;
        }
        
    }

    public void Trigger()
    {
        if(traps.Count != 0)
        {
            foreach (GameObject t in traps)
            {
                if(spikesHidden)
                {
                    t.GetComponent<HiddenTrap>().UnHide();
                }
            }
        }
        falling = true;
    }

    void SetCollider()
    {
        trapCollider.size = new Vector2(width, height);
        trapCollider.offset = new Vector2(((float)width - 1) / 2.0f, ((float)height - 1) / 2.0f);
        GetComponentInChildren<FallingWallTrigger>().SizeCollider(height);
    }

    private void ResetTrap()
    {
        falling = false;

        transform.eulerAngles = Vector3.zero;
        curRotZ = transform.localRotation.eulerAngles.z;
    }

    void GenerateWall()
    {
        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                if(!(i == 0 && j == 0))
                {
                    GameObject newPiece = new GameObject("Wall Piece " + i);
                    newPiece.transform.parent = gameObject.transform;
                    newPiece.transform.localPosition = new Vector3(j, i, 0);
                    newPiece.AddComponent<SpriteRenderer>();
                    newPiece.GetComponent<SpriteRenderer>().sprite = wallSprite;
                }
                if(j == 0)
                {
                    GameObject newSpike;
                    if(spikesHidden)
                    {
                        newSpike = Instantiate(Resources.Load("Prefabs/Traps/HiddenSpike", typeof(GameObject))) as GameObject;
                       
                    } else
                    {
                        newSpike = Instantiate(Resources.Load("Prefabs/Traps/Spike", typeof(GameObject))) as GameObject;
                    }
                    newSpike.transform.parent = gameObject.transform;
                    newSpike.transform.localPosition = new Vector3(-1, i, 0);
                    newSpike.transform.eulerAngles = new Vector3(0, 0, 90);
                    traps.Add(newSpike);
                }
            }
           
        }
    }
}
