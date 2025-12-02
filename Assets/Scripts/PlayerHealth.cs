using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHealth = 7;
    public int Health;

    public Hearts Hearts;
    
    void Start()
    {
        Health = MaxHealth;
        Hearts.UpdateHP(Health);
    }

    public void TakeDamage(int amount)
    {
        Health

    }
}
