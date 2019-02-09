using UnityEngine;
using UnityEngine.UI;

public class TankHealthBad : MonoBehaviour
{
    public float startingHealth = 100f;


    private float currentHealth;
    private bool dead;


    private void OnEnable()
    {
        currentHealth = startingHealth;
        dead = false;
    }


    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0f && !dead)
        {
            OnDeath();
        }
    }


    private void OnDeath()
    {
        dead = true;

        gameObject.SetActive(false);
    }
}