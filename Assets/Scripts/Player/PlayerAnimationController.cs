using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    public event System.Action corpseThrowEvent;

    private Animator PlayerAnimator
    {
        set => playerAnimator = value;
        get
        {
            if (playerAnimator == null)
            {
                playerAnimator = GetComponent<Animator>();
            }
            return playerAnimator;
        }
    }
    private Animator playerAnimator;

    private void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
    }

    public void Jump()
    {
        PlayerAnimator.SetTrigger("Jump");
    }

    public void Throw()
    {
        PlayerAnimator.SetTrigger("Throw");
    }

    public void Pickup()
    {
        PlayerAnimator.SetTrigger("Pickup");
    }

    public void SetWalkState(bool walk)
    {
        PlayerAnimator.SetBool("Walk", walk);
    }

    public void SetHasBody(bool hasBody)
    {
        PlayerAnimator.SetBool("HasBody", hasBody);
    }

    //Called by an animation event
    private void ThrowCorpseEvent()
    {
        corpseThrowEvent?.Invoke();
    }
}
