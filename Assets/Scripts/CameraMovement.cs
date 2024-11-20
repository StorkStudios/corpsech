using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float cameraZ = -10;
    
    private Transform playerTransform;

    private void Awake()
    {
        MessageBroker.Instance.Events.AddListener(MessageBroker.EventType.PlayerSpawned, (args) =>
        {
            PlayerSpawner.PlayerSpawnedEventArgs spawnArgs = (PlayerSpawner.PlayerSpawnedEventArgs)args;
            playerTransform = spawnArgs.PlayerTransform;
        });
        MessageBroker.Instance.Events.AddListener(MessageBroker.EventType.PlayerDeath, (_) =>

        {
            playerTransform = FindObjectOfType<PlayerSpawner>().transform;
        });
        playerTransform = FindObjectOfType<PlayerSpawner>().transform;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, cameraZ);
    }
}
