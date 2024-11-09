using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MonoBehaviour
{
    [System.Serializable]
    private enum MovementDirection
    {
        Forward,
        Backward
    }

    [SerializeField]
    private Transform startLocation;
    [SerializeField]
    private Transform endLocation;

    [SerializeField]
    private float movementDuration;

    private float startTime;
    private float endTime;

    //It will be changed to opposite on Start() call
    private MovementDirection currentDirection = MovementDirection.Backward;


    private void Start()
    {
        StartMovement();
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
            transform.position = Vector3.Lerp(startLocation.position, endLocation.position, (Time.time - startTime) / movementDuration);
        }
        else
        {
            transform.position = Vector3.Lerp(endLocation.position, startLocation.position, (Time.time - startTime) / movementDuration);
        }
    }

    private void StartMovement()
    {
        startTime = Time.time;
        endTime = startTime + movementDuration;
        currentDirection = currentDirection == MovementDirection.Forward ? MovementDirection.Backward : MovementDirection.Forward;
        transform.position = currentDirection == MovementDirection.Forward ? startLocation.position : endLocation.position;
    }
}
