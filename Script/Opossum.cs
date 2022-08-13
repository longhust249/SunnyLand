using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum : Enermy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private float runLength = 10f;
    private bool facingLeft = true;
    private Collider2D col;

    [SerializeField] private LayerMask ground;

    protected override void Start()
    {
        base.Start();
        col = GetComponent<Collider2D>();
        leftCap = transform.position.x - 3;
        rightCap = transform.position.x + 3;
    }

    private void Update()
    {
        if (facingLeft)
        {
            if (transform.position.x > leftCap)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector2(1, 1);
                }
                if (col.IsTouchingLayers(ground))
                {
                    transform.position += new Vector3(-runLength * Time.deltaTime, rb.velocity.y);
                    anim.SetBool("Running", true);
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
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector2(-1, 1);
                }
                if (col.IsTouchingLayers(ground))
                {
                    transform.position += new Vector3(runLength * Time.deltaTime, rb.velocity.y);
                    anim.SetBool("Running", true);
                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }

    
}
