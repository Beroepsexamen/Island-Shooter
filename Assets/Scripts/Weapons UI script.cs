using UnityEngine;
using UnityEngine.UI;
public class WeaponsUIscript : MonoBehaviour
{
     
    public Image iconImage;

    public void UpdateIcon(Sprite newIcon)
    {
        iconImage.sprite = newIcon;
    }

    
}
