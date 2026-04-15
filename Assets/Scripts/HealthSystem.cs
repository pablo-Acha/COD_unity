using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public int team;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount, Transform attacker)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die(attacker);
        }
    }

    void Die(Transform attacker)
    {
        GameManager.instance.AddPoint(team);

        Respawn();
    }

   void Respawn()
    {
        currentHealth = maxHealth;
    
        transform.position = SpawnManager.instance.GetSpawnPoint(team);
    }
}