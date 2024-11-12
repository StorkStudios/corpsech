using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

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
    Rigidbody2D rigidbody;
    private float startTime;
    private float endTime;

    //It will be changed to opposite on Start() call
    private MovementDirection currentDirection = MovementDirection.Backward;

    private void Start()
    {
        StartMovement();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Time.time >= endTime)
        {
            StartMovement();
            return;
        }
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        if (currentDirection == MovementDirection.Forward)
        {
            rigidbody.MovePosition(Vector3.Lerp(startPosition.position, endPosition.position, (Time.time - startTime) / movementDuration));
        }
        else
        {
            rigidbody.MovePosition(Vector3.Lerp(endPosition.position, startPosition.position, (Time.time - startTime) / movementDuration));
        }
    }

    private void StartMovement()
    {
        startTime = Time.time;
        endTime = startTime + movementDuration;
        currentDirection = currentDirection == MovementDirection.Forward ? MovementDirection.Backward : MovementDirection.Forward;
        transform.position = currentDirection == MovementDirection.Forward ? startPosition.position : endPosition.position;
    }
}
