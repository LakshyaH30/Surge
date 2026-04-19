using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private int maxJumps = 2; // double jump
    [SerializeField] private float lieFlatValue = -2.4f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private int jumpsRemaining;
    private bool isGrounded;
    private bool isDead;

    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDead) return;

        CheckGrounded();
        UpdateAnimations();
    }

    private void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius,groundLayer);

        //Refil Jumps after when landing
        if(isGrounded)
            jumpsRemaining = maxJumps;
    }

    // Called by Unity Input System via a Player Input Component
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.started || isDead) return;

        if (jumpsRemaining > 0)
        {
            //Reset vertical Velocity before Jump
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpsRemaining--;
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        rb.linearVelocity = new Vector2(0f,rb.linearVelocity.y);

        rb.bodyType = RigidbodyType2D.Kinematic; // disable rb so player stops

        transform.position = new Vector3(
             transform.position.x,
             lieFlatValue,
             transform.position.z
            ); // setting player to lie down on ground 

        animator.SetFloat("Speed", 0f);
        animator.SetBool("IsGrounded", false);
        animator.SetBool("IsJumping",false);
        animator.SetTrigger("Die");

        GameManager.Instance.GameOver();

        Debug.Log("Player died!");
    }

    private void UpdateAnimations()
    {
        animator.SetFloat("Speed",1f);
        animator.SetBool("IsGrounded" , isGrounded);
        animator.SetBool("IsJumping",rb.linearVelocity.y > 0.1f);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            
        }
    }
}
