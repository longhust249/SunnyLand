using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enermy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private float jumpLength = 10f;
    [SerializeField] private float jumpHeight = 15f;
    private bool facingLeft = true;
    private Collider2D col;
     
    [SerializeField] private LayerMask ground;

    protected override void Start()
    {
        base.Start();
        col = GetComponent<Collider2D>();
               
    }

    private void Update()
    {
        
        if (anim.GetBool("Jumping"))
        {
            if (rb.velocity.y < .1)
            {
                anim.SetBool("Jumping", false);
                anim.SetBool("Falling", true);
            }
        }
        if (col.IsTouchingLayers(ground) && anim.GetBool("Falling"))
        {
            anim.SetBool("Falling", false);
        }
    }

    private void Move()
    {
        if (facingLeft)
        {
            if (transform.position.x > leftCap)
            {
                if (transform.localScale.x == -1)
                {
                    transform.localScale = new Vector2(1, 1);
                }
                if (col.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
            }
            else
            {
                facingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < rightCap)
            {
                if (transform.localScale.x == 1)
                {
                    transform.localScale = new Vector2(-1, 1);
                }
                if (col.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }
}
