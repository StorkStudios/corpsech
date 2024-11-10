using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public class PlayerDeathEventArgs : MessageBroker.EventArgs
    {
        public PlayerDeathEventArgs(Vector3 position)
        {
            Position = position;
        }

        public Vector3 Position { get; private set; }
        //Reason maybe?
    }

    [SerializeField]
    private GameObject ragdollPrefab;

    private bool isDead = false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Deadly"))
        {
            Die();
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Deadly"))
        {
            Die();
        }

        if (other.gameObject.CompareTag("Killzone"))
        {
            DieWithoutRagdoll();
        }
    }

    public void DieWithoutRagdoll()
    {
        if (isDead)
        {
            return;
        }
        MessageBroker.Instance.Events.Invoke(MessageBroker.EventType.PlayerDeath, new PlayerDeathEventArgs(transform.position));
        Destroy(gameObject);
        isDead = true;
    }

    public void Die()
    {
        if (isDead)
        {
            return;
        }
        SpawnRagdoll();
        DieWithoutRagdoll();
    }

    private void SpawnRagdoll()
    {
        Instantiate(ragdollPrefab, transform.position, transform.rotation);
    }
}
