using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ForceAnimation : MonoBehaviour
{
    public RuntimeAnimatorController forcedAnimation;
    public string targetString = "Dummy";

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag(targetString))
        {
            Animator animator = other.GetComponent<Animator>();

            if (animator == null) animator = other.gameObject.AddComponent<Animator>();

            animator.runtimeAnimatorController = forcedAnimation;
        }
    }
}
