using UnityEngine;
using TMPro;
using System.Collections;
public class Achievementpopup : MonoBehaviour
{
    public TMP_Text achievementText;
    public float showTime = 3f;

    void Start()
    {
        achievementText.gameObject.SetActive(false);
    }
    public void ShowAchievement(string message)
    {
        StopAllCoroutines();
        achievementText.text = message;
        achievementText.gameObject.SetActive(true);
        StartCoroutine(HideAfterTime());
    }

    IEnumerator HideAfterTime()
    {
        yield return new WaitForSeconds(showTime);
        achievementText.gameObject.SetActive(false);
    }
   
}
