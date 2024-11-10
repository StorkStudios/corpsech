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

    [SerializeField, ReadOnly]
    private bool facingRight;
    public bool FacingRight => facingRight;

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
        facingRight = rb.velocity.x > 0 || (rb.velocity.x == 0 && facingRight);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}
