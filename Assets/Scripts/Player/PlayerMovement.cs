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
    public bool canAttack = true;
    public bool hasAK = false;
    public bool hasPistol = false;

    [SerializeField]
    private AudioClip jumpSound; // Variable for the jump sound

    private AudioSource audioSource; // Audio source component to play sounds

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    private void Update()
    {
        if (!canMove) { return; }

        float horizontalInput = Input.GetAxis("Horizontal");

        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = Vector3.one;
        }

        if ((Input.GetKey(KeyCode.W) && grounded) || (Input.GetKey(KeyCode.UpArrow) && grounded))
        {
            Jump();
        }

        if (hasPistol)
        {
            anim.SetBool("walkWithPistol", Mathf.Abs(horizontalInput) > 0.01f);
            anim.SetBool("run", false);
        }
        else if (hasAK)
        {
            anim.SetBool("walkWithAK", Mathf.Abs(horizontalInput) > 0.01f);
            anim.SetBool("run", false);
        }
        else
        {
            anim.SetBool("run", Mathf.Abs(horizontalInput) > 0.01f);
            anim.SetBool("walkWithPistol", false);
            anim.SetBool("walkWithAK", false);
        }


        anim.SetBool("grounded", grounded);
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
        if (hasAK)
        {
            anim.SetBool("HasAK", true);
        } else if (hasPistol)
        {
            anim.SetBool("HasPistol", true);
        }
        else
        {
            anim.SetTrigger("jump");
        }

        grounded = false;
        
        if (jumpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
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
    
    public void ResetWeapons()
    {
        hasPistol = false;
        hasAK = false;
        anim.SetBool("HasPistol", false);
        anim.SetBool("HasAK", false);
    }

}
