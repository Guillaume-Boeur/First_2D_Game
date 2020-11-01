using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int curentHealth;

    public HealthBar healthBar;

    void Start()
    {
        curentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20);
        }
    }

    void TakeDamage(int damage)
    {
        curentHealth -= damage;
        healthBar.SetHealth(curentHealth);
    }
}
