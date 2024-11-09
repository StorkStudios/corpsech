using UnityEngine;

[RequireComponent(typeof(PlayerGroundDetector))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float moveSpeed = 5f;
    
    [SerializeField]
    private float jumpForce = 10f;
    
    [Header("Ground Check")]
    [SerializeField]
    private Transform groundCheck;
    
    [SerializeField]
    private LayerMask groundLayer;

    [Header("Jump config")]
    [SerializeField]
    private float maxJumpVelocity;

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

        if ((Input.GetButton("Jump") || Input.GetAxis("Vertical") > 0.1) &&
            groundDetector.IsGrounded &&
            Mathf.Abs(rb.velocity.y) <= maxJumpVelocity)
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
