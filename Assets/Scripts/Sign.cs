using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{
    PlayerControls controls;
    bool interactable = false;
    public GameObject Icon;

    public string text;

    // Start is called before the first frame update
    void Start()
    {
        interactable = false;
        Icon.SetActive(false);
        if(text.Length == 0)
        {
            text = "New Text";
        }
    }

    private void Awake()
    {
        controls = new PlayerControls();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Interact.performed += Use;
    }

    private void OnDisable()
    {
        controls.Player.Interact.performed -= Use;
        controls.Disable();
    }

    void Use(InputAction.CallbackContext value)
    {
        if (interactable)
        {
            Icon.SetActive(false);
            interactable = false;
            GameObject.FindGameObjectWithTag("DialogueController").GetComponent<DialogueController>().readText(text);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            interactable = true;
            Icon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            interactable = false;
            Icon.SetActive(false);
        }
    }
}
