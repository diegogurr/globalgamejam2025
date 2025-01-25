using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class OptionManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider volumeSlider;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
   
    [Header("Game Settings")]
    private float volume;
    private int resolutionIndex;
    private bool isFullscreen;

    private void Start()
    {
        // Carica le impostazioni salvate
        LoadSettings();

        // Imposta gli eventi di modifica per gli UI elements
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
        fullscreenToggle.onValueChanged.AddListener(OnFullscreenChanged);
    }

    private void LoadSettings()
    {
        // Carica le impostazioni precedenti (se esistono)
        volume = PlayerPrefs.GetFloat("Volume", 1f);
        resolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 0);
        isFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;

        // Imposta gli UI elements
        volumeSlider.value = volume;
        fullscreenToggle.isOn = isFullscreen;

        // Carica le risoluzioni disponibili
        resolutionDropdown.ClearOptions();
        var resolutions = Screen.resolutions;
        foreach (var res in resolutions)
        {
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData($"{res.width}x{res.height}"));
        }

        // Imposta la risoluzione corrente
        resolutionDropdown.value = resolutionIndex;
    }

    private void SaveSettings()
    {
        // Salva le impostazioni
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void OnVolumeChanged(float value)
    {
        volume = value;
        AudioListener.volume = volume; // Cambia il volume di sistema
        SaveSettings(); // Salva la modifica
    }

    private void OnResolutionChanged(int index)
    {
        resolutionIndex = index;
        var resolutions = Screen.resolutions;
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, isFullscreen);
        SaveSettings(); // Salva la modifica
    }

    private void OnFullscreenChanged(bool isFullscreen)
    {
        this.isFullscreen = isFullscreen;
        var resolutions = Screen.resolutions;
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, isFullscreen);
        SaveSettings(); // Salva la modifica
    }
    public void LoadMenu(){
        SceneManager.LoadScene(1);
    }
}

