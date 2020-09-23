using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoadScene : MonoBehaviour
{
    public GameObject[] objects;

    public static DontDestroyOnLoadScene instance;

    private void Awake()
    {
        if (instance)
        {
            Debug.LogWarning("Déjà un DontDestroyOnLoadScene");
            return;
        }

        instance = this;

        foreach (var element in objects)
            DontDestroyOnLoad(element);

    }

    /* Deplacer les éléments que l'on ne doit pas détruire vers les détruire */
    public void RemoveFromDontDestroyOnLoad()
    {
        foreach (var element in objects)
            SceneManager.MoveGameObjectToScene(element, SceneManager.GetActiveScene());
    }
    
}
