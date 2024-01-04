// LevelMusicScript.cs

using UnityEngine;

public class LevelMusicScript : MonoBehaviour
{
    public AudioClip defaultMusic;
    public AudioClip level1Music;
    public AudioClip level2Music;
    public AudioClip level3Music;
    public AudioClip level4Music;
    public AudioClip level5Music;
    public AudioClip level6Music;

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
                case "Level 3":
                    AudioManager.Instance.PlayLevelMusic(level3Music);
                    break;
                case "Level 4":
                    AudioManager.Instance.PlayLevelMusic(level4Music);
                    break;
                case "Level 5":
                    AudioManager.Instance.PlayLevelMusic(level5Music);
                    break;
                case "Level 6":
                    AudioManager.Instance.PlayLevelMusic(level6Music);
                    break;
                default:
                    AudioManager.Instance.PlayLevelMusic(defaultMusic);
                    break;
            }
        }
    }
}
