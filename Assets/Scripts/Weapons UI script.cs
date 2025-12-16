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

//must make an empty game object and throw this ui script on it and select the weapons ui image.