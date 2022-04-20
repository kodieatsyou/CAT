using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{

    public GameObject dialogueBox;
    public Text text;
    public float timeBetweenLetters;
    public float timeBeforeExit;
    public float timeBetweenPeriod;
    public Image esc_icon;

    string fulltext;
    string showedtext;

    PlayerControls controls;

    bool reading = false;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        dialogueBox.SetActive(false);
        reading = false;
        esc_icon.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Interact.performed += ExitDialogue;
    }

    private void OnDisable()
    {
        controls.Player.Interact.performed -= ExitDialogue;
        controls.Disable();
    }

    void ExitDialogue(InputAction.CallbackContext value)
    {
        if (reading == false && esc_icon.enabled == true)
        {
            dialogueBox.SetActive(false);
            esc_icon.enabled = false;
            text.text = "";
        }
        
    }

    public void readText(string textToRead)
    {
        dialogueBox.SetActive(true);
        reading = true;
        StartCoroutine(readLetters(textToRead));
    }

    IEnumerator readLetters(string letters)
    {
        for(int i = 0; i < letters.Length; i++)
        {
            text.text += letters[i];
            if(letters[i] == '.')
            {
                yield return new WaitForSeconds(timeBetweenLetters + timeBetweenPeriod);
            } else
            {
                yield return new WaitForSeconds(timeBetweenLetters);
            }   
        }
        yield return new WaitForSeconds(timeBeforeExit);
        reading = false;
        esc_icon.enabled = true;
    }
}
