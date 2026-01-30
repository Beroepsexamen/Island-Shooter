using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Achievements : MonoBehaviour
{
    public static Achievements instance;

    public bool secret1;
    public bool secret2;
    public bool secret3;
    public bool secret4;
    public bool allSecrets;
    public bool enemyKills5;
    public bool enemyKills10;
    public bool enemyKills15;
    public bool redDeGijzelaars;
    public bool vindAlleGuns;

    public Achievementpopup popup;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UnlockAchievement(string achievementName)
    {
        switch (achievementName)
        {
            case "secret1":
                TryUnlock(ref secret1, "Secret 1");
                CheckAllSecrets();
                break;

            case "secret2":
                TryUnlock(ref secret2, "Secret 2");
                CheckAllSecrets();
                break;

            case "secret3":
                TryUnlock(ref secret3, "Secret 3");
                CheckAllSecrets();
                break;

            case "secret4":
                TryUnlock(ref secret4, "Secret 4");
                CheckAllSecrets();
                break;

            case "allSecrets":
                TryUnlock(ref allSecrets, "All Secrets");
                break;

            case "enemyKills5":
                TryUnlock(ref enemyKills5, "Enemy Kills 5");
                break;

            case "enemyKills10":
                TryUnlock(ref enemyKills10, "Enemy Kills 10");
                break;

            case "enemyKills15":
                TryUnlock(ref enemyKills15, "Enemy Kills 15");
                break;

            case "redDeGijzelaars":
                TryUnlock(ref redDeGijzelaars, "Red De Gijzelaars");
                break;

            case "vindAlleGuns":
                TryUnlock(ref vindAlleGuns, "Vind Alle Guns");
                break;
        }
    }

    void CheckAllSecrets()
    {
        if (allSecrets) return;

        if (secret1 && secret2 && secret3 && secret4)
        {
            TryUnlock(ref allSecrets, "All Secrets");
        }
    }


    void TryUnlock(ref bool achievement, string displayName)
    {
        if (achievement) return;

        achievement = true;
        Debug.Log("Achievement unlocked: " + displayName);

        if (popup != null)
            popup.ShowAchievement("Achievement Unlocked!\n" + displayName);
    }
}