using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHealth = 7;
    public int Health;
    
    void Start()
    {
        Health = MaxHealth;
    }
}
