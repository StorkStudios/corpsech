using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform square;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float rotation;
    [SerializeField]
    private float firerate;
    private float lastShotTime;

    void Start()
    {
        lastShotTime = Time.time;
    }

    void Update()
    {
        rotation = rotation + (rotationSpeed * Time.deltaTime);

        square.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));

        if (Time.time - lastShotTime > firerate)
        {
            Instantiate(bullet, square.position, square.rotation);
            lastShotTime = Time.time;
        }
    }
}
