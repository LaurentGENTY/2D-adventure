using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAndSaveData : MonoBehaviour
{
    public static LoadAndSaveData instance;

    public void Awake()
    {
        if (instance)
        {
            Debug.LogWarning("Déjà un LoadAndSaveData");
            return;
        }

        instance = this;
    }

    void Start()
    {
        Inventory.instance.coinsCount = PlayerPrefs.GetInt("cointsCount", 0);
        Inventory.instance.UpdateTextUI();

        /* Garder la vie d'avant ??? */
        //PlayerHealth.instance.currentHealth = PlayerPrefs.GetInt("currentHealth", PlayerHealth.instance.maxHealth);
        //PlayerHealth.instance.healthBar.SetHealth(PlayerHealth.instance.currentHealth);
    }


    public void SaveData()
    {
        PlayerPrefs.SetInt("coinsCount", Inventory.instance.coinsCount);
        //PlayerPrefs.SetInt("currentHealth", PlayerHealth.instance.currentHealth);
        
        /* Enregistrer le niveau maximal atteint */
        if (CurrentSceneManager.instance.levelToUnlock > PlayerPrefs.GetInt("levelReached", 1))
            PlayerPrefs.SetInt("levelReached", CurrentSceneManager.instance.levelToUnlock);
    }


}
