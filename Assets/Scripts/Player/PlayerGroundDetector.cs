using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundDetector : MonoBehaviour
{
    [SerializeField]
    private Vector2 detectionSize;
    [SerializeField]
    private Transform detectionPosition;
    [SerializeField]
    private LayerMask groundLayers;

    public bool IsGrounded { get; private set; }
    private bool lastGroundedState = false;

    public event Action<bool> GroundStateChanged;

    private void FixedUpdate()
    {
        lastGroundedState = IsGrounded;
        IsGrounded = Physics2D.OverlapBox(detectionPosition.position, detectionSize, 0, groundLayers);
        if (IsGrounded != lastGroundedState)
        {
            GroundStateChanged?.Invoke(IsGrounded);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detectionPosition.position, detectionSize);
    }
}
