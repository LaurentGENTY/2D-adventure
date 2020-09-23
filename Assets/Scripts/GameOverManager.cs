using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;

    public static GameOverManager instance;

    public void Awake()
    {
        if (instance)
        { 
            Debug.LogWarning("Déjà un GameOverManager");
            return;
        }

        instance = this;
    }

    public void OnPlayerDeath()
    {
        /* Si le joueur est présent par défaut, cela veut dire qu'il y aura duplication dans la scene des objets */
        if (CurrentSceneManager.instance.isPlayerPresentByDefault)
            DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();

        gameOverUI.SetActive(true);
    }

    public void RetryButton()
    {
        /* On retire le nombre de pièces ramassé dans la scene */
        Inventory.instance.RemoveCoints(CurrentSceneManager.instance.coinsPickedUpInThisSceneCount);

        /* Recharge la scene */
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        /* Replace le joueur au spawn */
        
        /* Reactive les mouvements et lui rend sa vie */

        /* Ferme l'écran */
        gameOverUI.SetActive(false);

        /* On fait respawn le joueur */
        PlayerHealth.instance.Respawn();
    }

    public void MainMenuButton()
    {
        /* Si on revient au menu principal, on doit supprimer tous les objets */
        DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
