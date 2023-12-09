using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    private AudioSource audioSource;
    private AudioSource musicSource;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
            }
            return instance;
        }
    }

    void Start()
    {
        // Assuming you have AudioSource components attached to the AudioManager GameObject
        audioSource = GetComponent<AudioSource>();
        musicSource = GetComponentInChildren<AudioSource>();

        // Load initial configuration settings
        LoadConfigurationSettings();
    }

    void LoadConfigurationSettings()
    {
        // Load audio setting
        int audioSetting = PlayerPrefs.GetInt("AudioSetting", 1); // Default to 1 if not set
        SetAudioEnabled(audioSetting == 1);

        // Load music setting
        int musicSetting = PlayerPrefs.GetInt("MusicSetting", 1); // Default to 1 if not set
        SetMusicEnabled(musicSetting == 1);
    }

    public void SetAudioEnabled(bool isEnabled)
{
    if (audioSource != null)
    {
        audioSource.enabled = isEnabled;
        audioSource.volume = isEnabled ? 0.1f : 0f; // Set volume to 70% if enabled, otherwise 0
    }
}

public void SetMusicEnabled(bool isEnabled)
{
    if (musicSource != null)
    {
        if (isEnabled)
        {
            musicSource.Play();
        }
        else
        {
            musicSource.Stop();
        }

        musicSource.volume = isEnabled ? 0.1f : 0f; // Set volume to 70% if enabled, otherwise 0
    }
}

}
