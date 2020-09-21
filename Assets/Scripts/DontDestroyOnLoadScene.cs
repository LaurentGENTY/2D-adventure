using UnityEngine;

public class DontDestroyOnLoadScene : MonoBehaviour
{
    public GameObject[] objects;

    /* Lue dès le début */
    public void Awake()
    {
        foreach (var element in objects)
            DontDestroyOnLoad(element);

    }
    
}
