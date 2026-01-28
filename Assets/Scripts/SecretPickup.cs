using UnityEngine;

public class SecretPickup : MonoBehaviour
{


    public string achievementName;   // bv: "Secret1"
    public GameObject pickupTextUI;   // "Press E" UI

    private bool canPickup = false;
    private Achievements achievements;

    private void Start()
    {
        if (pickupTextUI != null)
            pickupTextUI.SetActive(false);

        achievements = Object.FindFirstObjectByType<Achievements>();
    }

    private void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            if (achievements != null)
            {
                achievements.UnlockAchievement(achievementName);
                pickupTextUI.SetActive(false);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = true;

            if (pickupTextUI != null)
                pickupTextUI.SetActive(true);
            
              
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = false;

            if (pickupTextUI != null)
                pickupTextUI.SetActive(false);
        }
    }
}
