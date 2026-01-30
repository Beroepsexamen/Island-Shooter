using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScreenScript : MonoBehaviour
{
    [SerializeField] private List<Toggle> achievementToggles;

    void Start()
    {
        // Unlock and show the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Set all achievement toggles based on unlocked achievements
        if (Achievements.instance != null)
        {
            Achievements.instance.UnlockAchievement("RedDeGijzelaars");

            achievementToggles[0].isOn = Achievements.instance.secret1;
            achievementToggles[1].isOn = Achievements.instance.secret2;
            achievementToggles[2].isOn = Achievements.instance.secret3;
            achievementToggles[3].isOn = Achievements.instance.secret4;
            achievementToggles[4].isOn = Achievements.instance.allSecrets;
            achievementToggles[5].isOn = Achievements.instance.enemyKills5;
            achievementToggles[6].isOn = Achievements.instance.enemyKills10;
            achievementToggles[7].isOn = Achievements.instance.enemyKills15;
            achievementToggles[8].isOn = Achievements.instance.redDeGijzelaars;
            achievementToggles[9].isOn = Achievements.instance.vindAlleGuns;
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Start scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
