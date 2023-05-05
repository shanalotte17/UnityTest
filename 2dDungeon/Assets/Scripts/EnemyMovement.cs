using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float chaseDistance = 5f;

    private Vector3 previousPosition;
    private bool isFacingRight = false;
    private Animator animator;
    private float attackRange;
    private int currentDestination = 0;
    private EnemyState enemyState;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyState = GetComponent<EnemyState>();
        attackRange = GetComponent<EnemyCombat>().GetAttackRange();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) <= chaseDistance)
        {
            enemyState.StartChasing();
        }
        else
        {
            enemyState.StartPatrouling();
        }
        if (Vector2.Distance(transform.position, playerTransform.position) <= attackRange)
        {
            enemyState.StartAttacking();
        }

        if(enemyState.IsAttacking() == false)
        {
            animator.SetBool("IsWalking", true);
        }

        if (enemyState.IsPatrouling())
        {
            Patroul();
        }
        if (enemyState.IsChasing()) 
        {
            Chase();
        }

        if(previousPosition.x > transform.position.x && isFacingRight == true)
        {
            Flip();
            isFacingRight = false;
        }

        if(previousPosition.x < transform.position.x && isFacingRight == false)
        {
            Flip();
            isFacingRight = true;
        }
        previousPosition = transform.position;
    }

    private void Patroul()
    {
       Vector2 moveTowards = Vector2.MoveTowards(transform.position, points[currentDestination].position, moveSpeed * Time.deltaTime);
       transform.position = moveTowards;


        if(Vector2.Distance(transform.position, points[currentDestination].position) < 0.2f)
        {
            if(currentDestination + 2 > points.Length)
            {
                currentDestination = 0;
            }
            else
            {
                currentDestination++;
            }
        }
    }

    private void Chase()
    {
        Vector3 side = Vector3.zero;
        if(transform.position.x > playerTransform.position.x)
        {
            side = Vector3.left;
        }
        if(transform.position.x < playerTransform.position.x)
        {
            side = Vector3.right;
        }

        transform.position += side * moveSpeed * Time.deltaTime;
    }

    public void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
