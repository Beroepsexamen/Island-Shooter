using Unity.VisualScripting;
using UnityEngine;
using TMPro;
public class Enemycount : MonoBehaviour
{
    public static Enemycount instance;

    [SerializeField] private GameObject enemyParent;

    public TMP_Text Gijzelnemertext;
    public int enemyCount;
    private int enemiesKilled;

    private void Start()
    {
        instance = this;
        enemyCount = enemyParent.transform.childCount;
        UpdateUI();
    }

    private void Update()
    {
        switch (enemiesKilled)
        {
            case 5:
                Achievements.Instance.UnlockAchievement("EnemyKills5");
                break;
            case 10:
                Achievements.Instance.UnlockAchievement("EnemyKills10");
                break;
            case 15:
                Achievements.Instance.UnlockAchievement("EnemyKills15");
                break;
        }
    }

    public void EnemyKilled() 
    {
        enemyCount--;
        enemiesKilled++;
        UpdateUI();
    }

    void UpdateUI()
    {
        Gijzelnemertext.text = "" + enemyCount;
    }
}
        
