using UnityEngine;

public class PlayerDeathScript : MonoBehaviour
{
    public PlayerHealth PlayerHealth;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth.Health = 0;
        }
    }
}
