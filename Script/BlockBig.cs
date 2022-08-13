using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBig : MonoBehaviour
{
    private Collider2D col;
    private Rigidbody2D rb;
    private bool isFalling;
    private void Start()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (rb.velocity.y < 0f)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enermy")
        {
            Enermy enermy = other.gameObject.GetComponent<Enermy>();
            if (isFalling)
            {
                enermy.JumpOn();
            }
        }
    }
    public void Crank()
    {        
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
