using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 7;
    public int health;

    public Hearts hearts;
    
    private void Start()
    {
        health = maxHealth;
        hearts.UpdateHP(health);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        hearts.UpdateHP(health);
    }
}
