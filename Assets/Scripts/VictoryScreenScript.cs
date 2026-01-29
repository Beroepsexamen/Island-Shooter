using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreenScript : MonoBehaviour
{
    [SerializeField] private List<Toggle> achievementToggles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Achievements.Instance != null)
        {
            Achievements.Instance.UnlockAchievement("RedDeGijzelaars");

            achievementToggles[0].isOn = Achievements.Instance.Secret1;
            achievementToggles[1].isOn = Achievements.Instance.Secret2;
            achievementToggles[2].isOn = Achievements.Instance.Secret3;
            achievementToggles[3].isOn = Achievements.Instance.Secret4;
            achievementToggles[4].isOn = Achievements.Instance.EnemyKills5;
            achievementToggles[5].isOn = Achievements.Instance.EnemyKills10;
            achievementToggles[6].isOn = Achievements.Instance.EnemyKills15;
            achievementToggles[7].isOn = Achievements.Instance.RedDeGijzelaars;
            achievementToggles[8].isOn = Achievements.Instance.VindAlleGuns;
            achievementToggles[9].isOn = Achievements.Instance.AllSecrets;
        }
    }
}
