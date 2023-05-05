using UnityEngine;

public class EnemyHP : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
