using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb; // declaring a variable, it's private so only this script can use it, it's good practice to do that
    private Animator anim;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f; // now in Unity, next to our player movement script we can see this value because we used [SerializeField]
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, running, jumping, falling }


    [SerializeField] private AudioSource jumpSoundEffect;


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {

        dirX = Input.GetAxisRaw("Horizontal"); // if it's GetAxis() the character will slide a little bit when the key is released but with GetAxisRaw() the speed drops to 0 instantly
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y); // vector 2 is x, y, more used in 2d games


        if (Input.GetButtonDown("Jump") && isGrounded()) //GetKeyDown() is used so we cannot just fly off by holding space which we can do with GetKey(). instead of GetKeyDown() i replaced it with GetButtonDown() and instead of "space" it's on "Jump"
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); //x, y, z coordinates. vector3() is editing the "transform" part on the right when you select the player. we use 14f because its better practice
        }

        UpdateAnimationState();
       
    }

    private void UpdateAnimationState()
    {

        MovementState state;

        if (dirX > 0f) // moving right
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f) // moving left
        {
            state = MovementState.running;
            sprite.flipX = true; // flips the character 180 degrees so he faces left when we're moving left
        }
        else
        {
            state = MovementState.idle; // after this the idle animation starts playing
        }

        if (rb.velocity.y > .1f) // checks if y coordinate is higher than 0.1 aka if we are jumping
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        

        anim.SetInteger("state", (int)state); // (int) turns the value of state into int
    }
    
    private bool isGrounded() // this function is called in the if statement where we were checking whether or not the jump button was pressed
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround); // first 2 arguments create a box around player that has the same shape as the box collider that's on the player, the 3rd argument is the rotation that doesn't matter here because we aren't rotating anything, the next 2 arguments move the box down just a tiny bit
    } // now when we run this function it returns true or false depeneding if we're touching the ground or not  
    
}
