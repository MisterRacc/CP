// ConfigurationScript.cs

using UnityEngine;
using UnityEngine.UI;

public class ConfigurationScript : MonoBehaviour
{
    public Toggle musicToggle;

    void Start()
    {
        LoadConfigurationSettings();
    }

    void LoadConfigurationSettings()
    {
        // Load music setting
        int musicSetting = PlayerPrefs.GetInt("MusicSetting", 1); // Default to 1 if not set
        musicToggle.isOn = (musicSetting == 1);

        // Set AudioManager music state
        AudioManager.Instance.SetMusicEnabled(musicToggle.isOn);
        Debug.Log($"Music Loaded: {musicToggle.isOn}");
    }

    public void SaveConfigurationSettings()
    {
        // Save music setting
        int musicSetting = musicToggle.isOn ? 1 : 0;
        PlayerPrefs.SetInt("MusicSetting", musicSetting);

        // Set AudioManager music state
        AudioManager.Instance.SetMusicEnabled(musicToggle.isOn);
        Debug.Log($"Music Saved: {musicToggle.isOn}");
    }
}
