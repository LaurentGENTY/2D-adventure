using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    /* Stocker les résolutions et les assigner au dropdown */
    public Dropdown resolutionsDropdown;
    Resolution[] resolutions;

    public Slider musicSlider;
    public Slider soundSlider;
    
    /* Au lancement du jeu on veut récupérer les différentes résolutions disponibles afin de les proposer dans une liste */
    public void Start()
    {
        /* Setup les sliders aux bonnes valeurs selon la valeur des mixer */
        audioMixer.GetFloat("music", out float musicValueForSlider);
        musicSlider.value = musicValueForSlider;

        audioMixer.GetFloat("sound", out float soundValueForSlider);
        soundSlider.value = soundValueForSlider;


        /* Récuperer les résolutions sans doublons */
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();

        resolutionsDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add(resolutions[i].width.ToString() + "*" + resolutions[i].height.ToString());

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                currentResolutionIndex = i;
        }

        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();

        Screen.fullScreen = true;
    }

    /* Gérer le volume musique */
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("music",volume);
    }
    
    /* Gérer le volume sounds effects */
    public void SetSoundVolume(float volume)
    {
        audioMixer.SetFloat("sound", volume);
    }

    /* Link onValueChanged sur le toggle avec la SettingsWindow link et charger la fonction */
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    /* Link onValueChanged sur le dropdown avec la SettingsWindow link et charger la fonction */
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
