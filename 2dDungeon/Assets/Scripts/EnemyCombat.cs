using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private int damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private LayerMask playerLayer;

    private Animator animator;
    private float nextTimeToAttack;
    private EnemyState enemyState;


    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyState = GetComponent<EnemyState>();
    }

    private void Update()
    {
        if (enemyState.IsAttacking())
        {
            if(Time.time >= nextTimeToAttack)
            {
                nextTimeToAttack = Time.time + 1f / attackSpeed;
                animator.SetTrigger("IsAttacking");
                animator.SetBool("IsWalking", false);
                StartCoroutine(Attack());
            }
        }
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.5f);
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackPoint.position, 1f, playerLayer);

        foreach(Collider2D targets in hitTargets)
        {
            targets.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }

    public float GetAttackRange()
    {
        return attackRange;
    }
}
