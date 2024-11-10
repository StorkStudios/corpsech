using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCorpsePickuper : MonoBehaviour
{
    [SerializeField]
    private Transform referencePoint;

    private CorpseController corpseToPickup;
    private float corpseToPickupPositionDelta = float.NegativeInfinity;

    private PlayerMovement movement;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
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

    private void HandleCorpseCollision(Collider2D other)
    {
        if (other.CompareTag("Corpse"))
        {
            CorpseController corpse = other.GetComponent<CorpseController>();
            float corpsePositionDelta = (corpse.ReferencePoint.position - referencePoint.position).x * (movement.FacingRight ? 1 : -1);
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
