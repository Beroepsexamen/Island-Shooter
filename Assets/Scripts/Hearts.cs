using UnityEngine;
using UnityEngine.UI;
public class Hearts : MonoBehaviour
{
    public Image hpImages;
    public Sprite[] hpSprites;

    // Update the heart display based on current HP
    public void UpdateHP(int currentHP)
    {
        currentHP = Mathf.Clamp(currentHP, 1, hpSprites.Length);

        hpImages.sprite = hpSprites[currentHP - 1];
    }
}
