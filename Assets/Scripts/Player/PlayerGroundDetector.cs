using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerCorpsePickuper))]
public class PlayerGroundDetector : MonoBehaviour
{
    [SerializeField]
    private Vector2 detectionSize;
    [SerializeField]
    private Transform detectionPosition;
    [SerializeField]
    private LayerMask groundLayers;
    [SerializeField]
    private float wallCheckLength;
    [SerializeField]
    private float wallCheckY;

    [SerializeField, ReadOnly]
    private Rigidbody2D currentMovingPlatform;
    public Rigidbody2D CurrentMovingPlatform => currentMovingPlatform;

    public bool IsGrounded { get; private set; }
    public bool IsStandingOnCorpse { get; private set; }
    private bool lastGroundedState = false;

    private PlayerCorpsePickuper pickuper;

    public event Action<bool> GroundStateChanged;

    private readonly Collider2D[] groundCollidersBuffer = new Collider2D[10];


    private void Start()
    {
        pickuper = GetComponent<PlayerCorpsePickuper>();
    }

    private void FixedUpdate()
    {
        lastGroundedState = IsGrounded;
        int groundCollidersCount = Physics2D.OverlapBoxNonAlloc(detectionPosition.position, detectionSize, 0, groundCollidersBuffer, groundLayers);
        RaycastHit2D rightHit = Physics2D.Raycast(transform.position + Vector3.up * wallCheckY, Vector3.right, wallCheckLength, LayerMask.GetMask("Ground"));
        RaycastHit2D leftHit = Physics2D.Raycast(transform.position + Vector3.up * wallCheckY, Vector3.left, wallCheckLength, LayerMask.GetMask("Ground"));
        int groundCollidersCountWithoutWalls = groundCollidersCount;
        currentMovingPlatform = null;
        for (int i = 0; i < groundCollidersCount; i++)
        {
            if (groundCollidersBuffer[i] == rightHit.collider)
            {
                groundCollidersCountWithoutWalls--;
            }
            else if (groundCollidersBuffer[i] == leftHit.collider)
            {
                groundCollidersCountWithoutWalls--;
            }
            if (groundCollidersBuffer[i].gameObject.CompareTag("MovingPlatform"))
            {
                currentMovingPlatform = groundCollidersBuffer[i].GetComponent<Rigidbody2D>();
            }
        }
        IsGrounded = groundCollidersCountWithoutWalls > 0;
        if (IsGrounded != lastGroundedState)
        {
            GroundStateChanged?.Invoke(IsGrounded);
        }
        pickuper.HandleCorpseCollisions(groundCollidersBuffer, groundCollidersCount);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detectionPosition.position, detectionSize);
        Gizmos.DrawLine(transform.position + Vector3.up * wallCheckY, transform.position + Vector3.up * wallCheckY + Vector3.right * wallCheckLength);
        Gizmos.DrawLine(transform.position + Vector3.up * wallCheckY, transform.position + Vector3.up * wallCheckY + Vector3.left * wallCheckLength);
    }
}
