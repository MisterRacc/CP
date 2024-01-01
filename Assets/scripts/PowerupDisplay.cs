using UnityEngine;
using UnityEngine.UI;

public class PowerupDisplay : MonoBehaviour
{
    public Image powerupImage;

    public Sprite energyGelSprite;
    public Sprite invisibilitySprite;
    public Sprite fireResistanceSprite;


    public void DisplayPowerup(string name){

        switch (name)
        {
            case "Energy Gel":
                powerupImage.sprite = energyGelSprite;
                break;
            case "Fire Resistance Potion":
                powerupImage.sprite = fireResistanceSprite;
                break;
            case "Invisible Potion":
                powerupImage.sprite = invisibilitySprite;
                break;
            default:
                break;
        }
        powerupImage.enabled = true;
    }

    void Start()
    {
       powerupImage.enabled = false;
    }
    

    void Update()
    {
        
    }

}