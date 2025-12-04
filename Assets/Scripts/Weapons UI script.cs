using UnityEngine;
using UnityEngine.UI;
public class WeaponsUIscript : MonoBehaviour
{
     
    public ImageConversion iconImage;

    public void UpdateIcon(Sprite newIcon)
    {
        iconImage.sprite = newIcon;
    }

    
}
