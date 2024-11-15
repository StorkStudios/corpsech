using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public class PlayerSpawnedEventArgs : MessageBroker.EventArgs
    {
        public PlayerSpawnedEventArgs(Transform playerTransform)
        {
            PlayerTransform = playerTransform;
        }

        public Transform PlayerTransform { get; private set; }
    }

    [SerializeField]
    private GameObject playerPrefab;

    private void Start()
    {
        SpawnPlayer();
        MessageBroker.Instance.Events.AddListener(MessageBroker.EventType.PlayerDeath, (_) => SpawnPlayer());
    }

    private void SpawnPlayer()
    {
        GameObject playerObject = Instantiate(playerPrefab, transform.position, transform.rotation);
        MessageBroker.Instance.Events.Invoke(MessageBroker.EventType.PlayerSpawned, new PlayerSpawnedEventArgs(playerObject.transform));
    }
}
