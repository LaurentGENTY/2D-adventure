using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Button[] levels;

    public void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        /* Griser les boutons de base */
        for (int i = 0; i < levels.Length; i++)
            if (i + 1 > levelReached)
                levels[i].interactable = false;
    }

    public void LoadLevelPassed(string levelName)
    {
    
        SceneManager.LoadScene(levelName);
    }
}
