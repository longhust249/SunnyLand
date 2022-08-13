using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D col;
    private BlockBig blockBig;
    private CrankUp crankUp;
    
   
      
    

    private enum State { idle, running, jumping, falling, hurt, climb, crouch }
    private State state = State.idle;

    private float vertical;
    private bool isLadder;
    float hDirection;
    private bool isCrank;



    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float crouchSpeed = 2.5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int cherries = 0;
    [SerializeField] private Text cherryText;
    [SerializeField] private float hurtForce = 10f;
    [SerializeField] private AudioSource cherry;
    [SerializeField] private AudioSource footstep;
    [SerializeField] private int health;
    [SerializeField] private Text healthText;
    [SerializeField] private AudioSource jumpsound;
    


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        blockBig = GameObject.FindGameObjectWithTag("Blockbig").GetComponent<BlockBig>();
        crankUp = GameObject.FindGameObjectWithTag("Crank").GetComponent<CrankUp>();
        
        
        footstep = GetComponent<AudioSource>();
        healthText.text = health.ToString();              
    }

    private void Update()
    {
        if (state != State.hurt)
        {
            Movement();
        }
        VelocityState();
        anim.SetInteger("state", (int)state);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            cherry.Play();
            Destroy(collision.gameObject);
            cherries += 1;
            cherryText.text = cherries.ToString();
        }
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
        if (collision.CompareTag("Crank"))
        {
            isCrank = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
        }
        if (collision.CompareTag("Crank"))
        {
            isCrank = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enermy")
        {
            Enermy enermy = other.gameObject.GetComponent<Enermy>();
            if (state == State.falling)
            {
                enermy.JumpOn();
                
                Jump();
            }
            else
            {
                state = State.hurt;
                HandleHealth();
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }

        }
    }

    private void HandleHealth()
    {
        health -= 1;
        healthText.text = health.ToString();
        if (health <= 0)
        {
            PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene("TheEnd");
        }
    }

    private void Movement()
    {

        hDirection = Input.GetAxis("Horizontal");
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        if (Input.GetButtonDown("Jump") && col.IsTouchingLayers(ground))
        {
            jumpsound.Play();
            Jump();
        }
        if (Input.GetButton("crouch") && col.IsTouchingLayers(ground))
        {
            Crouch();
            if (isCrank)
            {                    
                blockBig.Crank();
                crankUp.CrankDown();
                
            }
        }
        else if (Input.GetButtonUp("crouch") && col.IsTouchingLayers(ground))
        {
            state = State.idle;
        }
        vertical = Input.GetAxis("Vertical");
        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            Climb();
        }
        else
        {
            rb.gravityScale = 4f;
        }

    }


    private void Crouch()
    {
        rb.velocity = new Vector2(hDirection * crouchSpeed, rb.velocity.y);
        state = State.crouch;
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;
    }
    private void VelocityState()
    {
        if (state == State.crouch)
        {

        }
        else if (state == State.climb)
        {
            if (Mathf.Abs(rb.velocity.x) > 2f)
            {
                state = State.running;
            }
        }

        else if (state == State.jumping)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (col.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }

        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }

    }
    public void FootStep()
    {
        footstep.Play();
    }
    public void Climb()
    {
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        state = State.climb;
    }
    

}
