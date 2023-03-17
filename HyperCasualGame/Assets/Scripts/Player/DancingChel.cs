using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingChel : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Dancing", true);
    }
}
