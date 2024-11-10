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
        IsGrounded = groundCollidersCount > 0;
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
    }
}
