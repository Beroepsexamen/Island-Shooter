using UnityEngine;

public class PlayerDeathScript : MonoBehaviour
{
    public PlayerHealth playerHealth;

    void OnCollisionEnter(Collision collision)
    {
        // If the parent object collides with the player, kill the player
        if (collision.gameObject.CompareTag("Player")) 
        {
            playerHealth.health = 0;
        }
    }
}
