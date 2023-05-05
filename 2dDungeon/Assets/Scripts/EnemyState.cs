using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    private bool isPatrouling = true;
    private bool isChasing = false;
    private bool isAttacking = false;

    public bool IsPatrouling()
    {
        return isPatrouling;
    }

    public bool IsChasing()
    {
        return isChasing;
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }

    public void StartPatrouling()
    {
        isAttacking = false;
        isChasing = false;
        isPatrouling = true;
    }

    public void StartChasing()
    {
        isAttacking = false;
        isPatrouling = false;
        isChasing = true;
    }

    public void StartAttacking()
    {
        isPatrouling = false;
        isChasing = false;
        isAttacking = true;
    }
}
