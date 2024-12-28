using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    public bool canMove = true;
    


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!canMove) { return; }
        float horizontalInput = Input.GetAxis("Horizontal");
        
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        //flip player when moving left-right
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            canMove = true;
        } else if (horizontalInput < -0.01f)
        {
            transform.localScale = Vector3.one;
            canMove = true;
        }
        
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }
        
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
        
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
        anim.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
    
    public bool IsGrounded()
    {
        return grounded;
    }

}
