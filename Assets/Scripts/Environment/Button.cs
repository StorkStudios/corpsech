using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    [SerializeField]
    private UnityEvent buttonPress;
    [SerializeField]
    private UnityEvent buttonRelease;

    private HashSet<GameObject> buttonItems = new HashSet<GameObject>();

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Corpse") || trigger.gameObject.CompareTag("Player"))
        {
            buttonItems.Add(trigger.gameObject);

            if (buttonItems.Count == 1)
            {
                buttonPress.Invoke();
            }
        }
    }

    void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Corpse") || trigger.gameObject.CompareTag("Player"))
        {
            buttonItems.Remove(trigger.gameObject);

            if (buttonItems.Count == 0)
            {
                buttonRelease.Invoke();
            }
        }
    }
}
