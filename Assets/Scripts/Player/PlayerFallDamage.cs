using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerDeath))]
public class PlayerFallDamage : MonoBehaviour
{
    [SerializeField]
    private float thresholdVelocity;

    private Rigidbody2D rb;
    private PlayerDeath playerDeath;
    private float lastYVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerDeath = GetComponent<PlayerDeath>();
    }

    private void FixedUpdate()
    {
        lastYVelocity = rb.velocity.y;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") &&
            lastYVelocity < thresholdVelocity)
        {
            playerDeath.Die();
        }
    }
}
