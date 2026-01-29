using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    #region Singleton

    public static PlayerHealth instance;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion

    public int maxHealth = 7;
    public int health;

    public Hearts hearts;
    
    private void Start() // Initialize health and update hearts display
    {
        health = maxHealth;
        hearts.UpdateHP(health);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamageP(1);
        }
    }

    public void TakeDamageP(int amount) // Reduce health and update hearts display
    {
        health -= amount;

        hearts.UpdateHP(health);
    }
}
