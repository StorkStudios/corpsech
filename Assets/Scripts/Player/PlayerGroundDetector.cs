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

    private void FixedUpdate()
    {
        IsGrounded = Physics2D.OverlapBox(detectionPosition.position, detectionSize, 0, groundLayers);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detectionPosition.position, detectionSize);
    }
}
