// AudioManager.cs

using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    private AudioSource musicSource;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "AudioManager";
                    instance = obj.AddComponent<AudioManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        musicSource = gameObject.AddComponent<AudioSource>();
        int musicSetting = PlayerPrefs.GetInt("MusicSetting", 1); // Default to 1 if not set
        SetMusicEnabled(musicSetting == 1);
    }

    public void SetMusicEnabled(bool isEnabled)
{
    if (musicSource != null)
    {
        if (isEnabled)
        {
            musicSource.Play();
            Debug.Log("Music Enabled");
        }
        else
        {
            musicSource.Stop();
            Debug.Log("Music Disabled");
        }
    }
}


    public void PlayLevelMusic(AudioClip levelMusic)
    {
        if (levelMusic != null)
        {
            musicSource.clip = levelMusic;
            musicSource.Play();
        }
    }

    public void StopLevelMusic()
    {
        musicSource.Stop();
    }
}
