using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Achievements : MonoBehaviour
{
    public static Achievements Instance;

    public bool Secret1;
    public bool Secret2;
    public bool Secret3;
    public bool Secret4;
    public bool AllSecrets;
    public bool EnemyKills5;
    public bool EnemyKills10;
    public bool EnemyKills15;
    public bool RedDeGijzelaars;
    public bool VindAlleGuns;

    public Achievementpopup popup;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
            case "Secret1":
                TryUnlock(ref Secret1, "Secret 1");
                break;

            case "Secret2":
                TryUnlock(ref Secret2, "Secret 2");
                break;

            case "Secret3":
                TryUnlock(ref Secret3, "Secret 3");
                break;

            case "Secret4":
                TryUnlock(ref Secret4, "Secret 4");
                break;

            case "AllSecrets":
                TryUnlock(ref AllSecrets, "All Secrets");
                break;

            case "EnemyKills5":
                TryUnlock(ref EnemyKills5, "EnemyKills5");
                break;

            case "EnemyKills10":
                TryUnlock(ref EnemyKills10, "EnemmyKills10");
                break;

            case "EnemyKills15":
                TryUnlock(ref EnemyKills15, "EnemyKills15");
                break;

            case "RedDeGijzelaars":
                TryUnlock(ref RedDeGijzelaars, "RedDeGijzelaars");
                break;

            case "VindAlleGuns":
                TryUnlock(ref VindAlleGuns, "VindAlleGuns");
                break;
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