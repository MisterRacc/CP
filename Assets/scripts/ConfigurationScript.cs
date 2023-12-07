using UnityEngine;
using UnityEngine.UI;

public class ConfigurationScript : MonoBehaviour
{
    public Toggle audioToggle;
    public Toggle musicToggle;

    public void SaveConfigurationSettings()
    {
        // Save audio setting
        int audioSetting = audioToggle.isOn ? 1 : 0;
        PlayerPrefs.SetInt("AudioSetting", audioSetting);

        // Save music setting
        int musicSetting = musicToggle.isOn ? 1 : 0;
        PlayerPrefs.SetInt("MusicSetting", musicSetting);
    }
}
