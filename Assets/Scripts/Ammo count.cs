using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    public TMP_Text ammoText;

    public void UpdateAmmo(int clip, int reserve)
    {
        ammoText.text = clip + " / " + reserve;
    }
}