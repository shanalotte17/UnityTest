using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackSpeed = 1f;

    private float nextTimeToAttack;
    private Animator playerAnimator;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextTimeToAttack && GetComponent<PlayerMovement>().IsGrounded())
        {
            nextTimeToAttack = Time.time + 1f / attackSpeed;
            Attack();
        }
    }

    private void Attack()
    {
        playerAnimator.SetTrigger("Attack");

        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach(Collider2D enemy in enemies)
        {
            enemy.GetComponent<IDamageable>().TakeDamage(attackDamage);
        }
    }
}
