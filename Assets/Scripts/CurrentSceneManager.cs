using UnityEngine;

/* Permet de gérer le cas de la destruction et duplication des objets lors d'une mort du joueur */
public class CurrentSceneManager : MonoBehaviour
{
    public int coinsPickedUpInThisSceneCount;

    public Vector3 respawnPoint;

    public static CurrentSceneManager instance;

    public int levelToUnlock;

    private void Awake()
    {
        if (instance)
        {
            Debug.LogWarning("Déjà un CurrentSceneManager");
            return;
        }

        instance = this;

        respawnPoint = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

}
