using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{

    PlayerControls controls;
    GameObject player;
    bool paused;
    public GameObject pauseUI;
    public GameObject gameUI;
    public float respawnTimer = 1f;

    public int deaths;

    GameObject checkpoint;
    GameObject[] traps;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.BringUpMenu.performed += Pause;
    }

    private void OnDisable()
    {
        controls.Disable();
        controls.Player.BringUpMenu.performed -= Pause;
    }

    void Pause(InputAction.CallbackContext value)
    {
        if(!paused)
        {
            paused = true;
            gameUI.SetActive(false);
            pauseUI.SetActive(true);

            Time.timeScale = 0;
        } else
        {
            paused = false;
            gameUI.SetActive(true);
            pauseUI.SetActive(false);

            Time.timeScale = 1;
        }
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCountDown());
    }

    IEnumerator RespawnCountDown()
    {
        yield return new WaitForSeconds(respawnTimer);
        player.transform.position = checkpoint.transform.position;
        player.GetComponent<PlayerController>().Spawn();
        if (checkpoint.tag == "StartingPoint")
        {
            //Start point stuff
        } else
        {
            checkpoint.GetComponent<Checkpoint>().PlaySpawnAnimation();
        }
        deaths++;
        ResetTraps();
    }

    void ResetTraps()
    {
        EventManager.instance.OnResetTraps();
    }

    public void SetCheckPoint(GameObject newCheckpoint)
    {
        if(checkpoint != null && checkpoint.tag != "StartingPoint")
        {
            checkpoint.GetComponent<Checkpoint>().DeActivateCheckPoint();
        }
        checkpoint = newCheckpoint;
    }

    public void ResumeGame()
    {
        if (!paused)
        {
            return;
        }
        else
        {
            paused = false;
            gameUI.SetActive(true);
            pauseUI.SetActive(false);

            Time.timeScale = 1;
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
    }

    public void BringUpOptions()
    {
        Debug.Log("Options");
    }

    // Start is called before the first frame update
    void Start()
    {
        gameUI.SetActive(true);
        pauseUI.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        traps = GameObject.FindGameObjectsWithTag("Hidden");
        checkpoint = GameObject.FindGameObjectWithTag("StartingPoint");
        Time.timeScale = 1;
        Respawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
