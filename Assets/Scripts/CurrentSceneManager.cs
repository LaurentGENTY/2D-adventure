using UnityEngine;

/* Permet de gérer le cas de la destruction et duplication des objets lors d'une mort du joueur */
public class CurrentSceneManager : MonoBehaviour
{
    public bool isPlayerPresentByDefault = false;

    public int coinsPickedUpInThisSceneCount;

    public static CurrentSceneManager instance;
    private void Awake()
    {
        if (instance)
        {
            Debug.LogWarning("Déjà un CurrentSceneManager");
            return;
        }

        instance = this;
    }

}
