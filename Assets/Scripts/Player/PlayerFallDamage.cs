using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerDeath))]
[RequireComponent(typeof(PlayerGroundDetector))]
public class PlayerFallDamage : MonoBehaviour
{
    [SerializeField]
    private float thresholdVelocity;

    private Rigidbody2D rb;
    private PlayerDeath playerDeath;
    private PlayerGroundDetector groundDetector;
    private float lastYVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerDeath = GetComponent<PlayerDeath>();
        groundDetector = GetComponent<PlayerGroundDetector>();
        groundDetector.GroundStateChanged += OnGroundStateChanged;
    }

    private void FixedUpdate()
    {
        lastYVelocity = rb.velocity.y;
    }

    private void OnGroundStateChanged(bool groundState)
    {
        if (groundState && lastYVelocity < thresholdVelocity)
        {
            playerDeath.Die();
        }
    }
}
