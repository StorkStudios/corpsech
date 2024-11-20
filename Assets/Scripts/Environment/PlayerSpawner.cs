using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
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

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SpawnPlayer();
        MessageBroker.Instance.Events.AddListener(MessageBroker.EventType.PlayerDeath, (_) => SpawnPlayer());
    }

    private void SpawnPlayer()
    {
        spriteRenderer.enabled = true;
        animator.Play("PlayerSpawn");
    }

    //Called by an animation event
    private void PlayerSpawnEndEvent()
    {
        GameObject playerObject = Instantiate(playerPrefab, transform.position, transform.rotation);
        MessageBroker.Instance.Events.Invoke(MessageBroker.EventType.PlayerSpawned, new PlayerSpawnedEventArgs(playerObject.transform));
        spriteRenderer.enabled = false;
    }
}
