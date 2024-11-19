using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCorpsePickuper : MonoBehaviour
{
    [Header("Config")]
    [SerializeField]
    private Transform pickupReferencePoint;
    [SerializeField]
    private Transform throwReferencePoint;
    [SerializeField]
    private Vector3 throwForce;
    [SerializeField]
    private GameObject corpsePrefab;

    [Header("References")]
    [SerializeField]
    private PlayerAnimationController animationController;


    private CorpseController corpseToPickup;
    private float corpseToPickupPositionDelta = float.NegativeInfinity;

    private PlayerMovement movement;

    [SerializeField, ReadOnly]
    private bool corpsePickedUp;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        animationController.corpseThrowEvent += ThrowCorpse;
    }

    private void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            if (corpseToPickup != null && !corpsePickedUp)
            {
                PickupCorpse();
            }
            else if (corpsePickedUp)
            {
                animationController.Throw();
            }
        }
    }

    public void HandleCorpseCollisions(Collider2D[] colliders, int collidersCount)
    {
        if (corpseToPickup != null)
        {
            corpseToPickup.SetHighlight(false);
        }
        corpseToPickup = null;
        corpseToPickupPositionDelta = float.NegativeInfinity;
        for (int i = 0; i < collidersCount; i++)
        {
            HandleCorpseCollision(colliders[i]);
        }
        if (corpseToPickup != null)
        {
            corpseToPickup.SetHighlight(true);
        }
    }

    private void PickupCorpse()
    {
        corpsePickedUp = true;
        Destroy(corpseToPickup.gameObject);
        corpseToPickup = null;
        corpseToPickupPositionDelta = float.NegativeInfinity;
        animationController.Pickup();
        animationController.SetHasBody(true);
    }

    private void ThrowCorpse()
    {
        GameObject corpse = Instantiate(corpsePrefab, throwReferencePoint.position, Quaternion.identity);
        Vector3 force = throwForce;
        force.x = movement.FacingRight ? throwForce.x : -throwForce.x;
        corpse.GetComponent<Rigidbody2D>().AddForce(force);
        corpsePickedUp = false;
        animationController.SetHasBody(false);
    }

    private void HandleCorpseCollision(Collider2D other)
    {
        if (other.CompareTag("Corpse"))
        {
            CorpseController corpse = other.GetComponent<CorpseController>();
            float corpsePositionDelta = (corpse.ReferencePoint.position - pickupReferencePoint.position).x * (movement.FacingRight ? 1 : -1);
            if (corpsePositionDelta > 0)
            {
                if (corpseToPickupPositionDelta < 0 || corpseToPickupPositionDelta > corpsePositionDelta)
                {
                    corpseToPickup = corpse;
                    corpseToPickupPositionDelta = corpsePositionDelta;
                }
            }
            else
            {
                if (corpseToPickupPositionDelta < 0 && corpseToPickupPositionDelta < corpsePositionDelta)
                {
                    corpseToPickup = corpse;
                    corpseToPickupPositionDelta = corpsePositionDelta;
                }
            }
        }
    }
}
