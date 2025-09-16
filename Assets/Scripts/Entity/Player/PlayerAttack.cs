using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool isAttacking;
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetMouseButton(0) && !isAttacking)
        {
            animator.Play("Attack");
            isAttacking = true;
        }
        if (isAttacking)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1.0f)
            {
                animator.Play("Idle");
                isAttacking = false;
            }
        }
    }

}
