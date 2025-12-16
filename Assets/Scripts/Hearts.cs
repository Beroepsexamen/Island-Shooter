using UnityEngine;
using UnityEngine.UI;
public class Hearts : MonoBehaviour
{
    public Image hpImages;
    public Sprite[] hpSprites;

    public void UpdateHP(int currentHP)
    {
        currentHP = Mathf.Clamp(currentHP, 1, hpSprites.Length);

        hpImages.sprite = hpSprites[currentHP - 1];
    }
}
