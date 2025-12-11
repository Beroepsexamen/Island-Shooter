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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;

        Hearts.UpdateHP(Health);
    }
}
