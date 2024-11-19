using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    public event System.Action corpseThrowEvent;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Jump()
    {
        animator.SetTrigger("Jump");
    }

    public void Throw()
    {
        animator.SetTrigger("Throw");
    }

    public void Pickup()
    {
        animator.SetTrigger("Pickup");
    }

    public void SetWalkState(bool walk)
    {
        animator.SetBool("Walk", walk);
    }

    public void SetHasBody(bool hasBody)
    {
        animator.SetBool("HasBody", hasBody);
    }

    //Called by an animation event
    private void ThrowCorpseEvent()
    {
        corpseThrowEvent?.Invoke();
    }
}
