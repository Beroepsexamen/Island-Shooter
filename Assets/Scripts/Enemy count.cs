using Unity.VisualScripting;
using UnityEngine;
using TMPro;
public class Enemycount : MonoBehaviour
{
    public static Enemycount instance;

    [SerializeField] private GameObject enemyParent;

    public TMP_Text Gijzelnemertext;
    public int enemyCount;

    private void Start()
    {
        instance = this;
        enemyCount = enemyParent.transform.childCount;
    }

    private void Awake()
    {
        UpdateUI();
    }

    public void EnemyKilled() 
    {
        enemyCount--;
        UpdateUI();
    }

    void UpdateUI()
    {
        Gijzelnemertext.text = "" + enemyCount;
    }
}
        
