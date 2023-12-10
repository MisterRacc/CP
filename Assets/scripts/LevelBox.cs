using UnityEngine;
using UnityEngine.UI;

public class LevelBox : MonoBehaviour
{
    public int levelNumber; // Set this in the inspector for each box (1 for Box_lvl1, 2 for Box_lvl2, etc.)
    public Sprite completedImage; // Set this in the inspector to the image you want for completed levels
    public Sprite openImage; // Set this in the inspector to the image you want for open but not completed levels
    public Sprite lockedImage; // Set this in the inspector to the image you want for locked levels

    private Image boxImage;

    void Start()
    {
        boxImage = GetComponent<Image>();
        UpdateBoxImage();
    }

    void UpdateBoxImage()
    {
        int previousLevelState = PlayerPrefs.GetInt("Level" + (levelNumber - 1) + "Completed", 0);
        int currentLevelState = PlayerPrefs.GetInt("Level" + levelNumber + "Completed", 0);

        // Check if the previous level is completed, change the image to openImage if true
        if (previousLevelState == 2)
        {
            boxImage.sprite = openImage;
        }
        // If the current level is completed, change the image to completedImage
        if (currentLevelState == 2)
        {
            boxImage.sprite = completedImage;
        }
        if(currentLevelState==1){
            boxImage.sprite = openImage;
        }
        // If the current level is not completed, change the image to lockedImage
        if(currentLevelState==0)
        {
            boxImage.sprite = lockedImage;
        }
    }
}
