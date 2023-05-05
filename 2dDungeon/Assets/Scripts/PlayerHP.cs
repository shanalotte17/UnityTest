using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHP = 100;

    private int currentHP;

    private void Start()
    {
        currentHP = maxHP;
    }

    private void Die()
    {
        Debug.Log("Player Is Dead");
    }

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;
        if(currentHP <= 0)
        {
            Die();
        }
    }
}
