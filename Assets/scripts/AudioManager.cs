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
        }
    }
}
