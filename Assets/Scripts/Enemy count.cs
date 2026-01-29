using Unity.VisualScripting;
using UnityEngine;
using TMPro;
public class Enemycount : MonoBehaviour
{
  public TMP_Text Gijzelnemertext;
    private int killCount = 0;

    private void Start()
    {
        UpdateUI();
    }

    public void EnemyKilled() 
    {
        killCount++;
        UpdateUI();
    }

    void UpdateUI()
    {
        Gijzelnemertext.text = "" + killCount;

    }
}
        
