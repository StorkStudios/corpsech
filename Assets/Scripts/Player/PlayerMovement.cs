using UnityEngine;

[RequireComponent(typeof(PlayerGroundDetector))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    
    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private float horizontalInput;
    private PlayerGroundDetector groundDetector;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundDetector = GetComponent<PlayerGroundDetector>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if ((Input.GetButton("Jump") || Input.GetAxis("Vertical") > 0.1) && groundDetector.IsGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}
