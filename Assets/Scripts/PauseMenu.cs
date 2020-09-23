using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/* On le met sur le GameManager object car on veut pouvoir l'appeler depuis n'importe quelle scene */
public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseMenuUI;
    public GameObject settingsWindow;


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        /* Desactiver le script de déplacement */
        PlayerMouvement.instance.enabled = false;

        /* Activer le menu pause */
        pauseMenuUI.SetActive(true);

        /* Arrêter le temps */
        Time.timeScale = 0;

        /* Changer l'état du jeu */
        isPaused = true;
    }

    /* Inverse */
    public void Resume()
    {
        PlayerMouvement.instance.enabled = true;

        pauseMenuUI.SetActive(false);

        Time.timeScale = 1;

        isPaused = false;
    }

    public void OpenSettingsWindow()
    {
        settingsWindow.SetActive(true);
    }
    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false);
    }

    public void LoadMainMenu()
    {
        /* Si on revient au menu principal, on doit supprimer tous les objets */
        DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();

        Resume();

        SceneManager.LoadScene("MainMenu");
    }

}
