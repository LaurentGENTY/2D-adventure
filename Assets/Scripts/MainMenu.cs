using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;

    public GameObject settingsWindow;

    public void StartGameButton()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    
    public void SettingsButton()
    {
        settingsWindow.SetActive(true);

    }

    public void CloseSettingsButton()
    {
        settingsWindow.SetActive(false);
    }
    
    public void QuitButton()
    {
        Application.Quit();
    }
}
