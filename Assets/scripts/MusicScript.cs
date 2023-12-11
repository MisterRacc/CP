// LevelMusicScript.cs

using UnityEngine;

public class LevelMusicScript : MonoBehaviour
{
    public AudioClip defaultMusic;
    public AudioClip level1Music;
    public AudioClip level2Music;

    void Start()
    {
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        PlayLevelMusic(currentSceneName);
    }

    void PlayLevelMusic(string sceneName)
    {
        // Access AudioManager and play level-specific music
        if(PlayerPrefs.GetInt("MusicSetting", -1)==1){
            switch (sceneName)
            {
                case "Level 1":
                    AudioManager.Instance.PlayLevelMusic(level1Music);
                    break;
                case "Level 2":
                    AudioManager.Instance.PlayLevelMusic(level2Music);
                    break;
                default:
                    // Default case, can be used for a general level or fallback music
                    AudioManager.Instance.PlayLevelMusic(defaultMusic);
                    break;
                // Add more cases for other levels as needed
            }
        }
    }
}
