using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;

    void Start()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);
    }

}
