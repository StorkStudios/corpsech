using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingPlatform : MonoBehaviour
{
    [System.Serializable]
    private enum MovementDirection
    {
        Forward,
        Backward
    }

    [SerializeField]
    private Transform startPosition;
    [SerializeField]
    private Transform endPosition;
    [SerializeField]
    private float movementDuration;

    private Rigidbody2D rigidbody;
    private float endTime;

    //It will be changed to opposite on Start() call
    private MovementDirection currentDirection = MovementDirection.Backward;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        StartMovement();
    }

    private void Update()
    {
        if (Time.time >= endTime)
        {
            StartMovement();
            return;
        }
    }

    private void StartMovement()
    {
        endTime = Time.time + movementDuration;
        currentDirection = currentDirection == MovementDirection.Forward ? MovementDirection.Backward : MovementDirection.Forward;
        rigidbody.MovePosition(currentDirection == MovementDirection.Forward ? startPosition.position : endPosition.position);
        rigidbody.velocity = (endPosition.position - startPosition.position).normalized * (endPosition.position - startPosition.position).magnitude / movementDuration;
        if (currentDirection == MovementDirection.Backward)
        {
            rigidbody.velocity *= -1;
        }
    }
}
