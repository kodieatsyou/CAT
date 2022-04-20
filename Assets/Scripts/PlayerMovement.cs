using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerControls playerControls;
    Vector2 movement;
    Animator anim;
    public float speed;
    PlayerController pc;
    Rigidbody2D rb2d;
    bool isGrounded;
    public Vector2 jumpHeight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pc = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.Player.Jump.performed += Jump;
    }

    private void OnDisable()
    {
        playerControls.Player.Jump.performed -= Jump;
        playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        movement = playerControls.Player.Movement.ReadValue<Vector2>();
        anim.SetFloat("Vertical", rb2d.velocity.y);
    }

    private void FixedUpdate()
    {
        if(pc.alive)
        {
            if (movement.x == 0)
            {
                anim.SetBool("Idle", true);
            }
            else
            {
                anim.SetBool("Idle", false);
                anim.SetFloat("Horizontal", movement.x);
            }
            rb2d.velocity = new Vector2(movement.x * speed, rb2d.velocity.y);
        }
    }

    void Jump(InputAction.CallbackContext value)
    {
        if(isGrounded && pc.alive)
        {
            //anim.SetBool("Jumping", true);
             rb2d.AddForce(jumpHeight, ForceMode2D.Impulse);
        }
    }

    public void Ground()
    {
        isGrounded = true;
        anim.SetBool("Grounded", true);
    }

    public void UnGround()
    {
        isGrounded = false;
        anim.SetBool("Grounded", false);
    }
}
