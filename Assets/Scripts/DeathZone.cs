using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour
{
    private Transform playerSpawn;
    public Animator fadeSystem;

    /* On récupère les variables qu'une fois dans le Awake afin que cela ne coûte pas trop cher */
    public void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* Si c'est le joueur qui rentre en contact alors on respawn (attention, ne pas abuser de GameObject.FindGameObjectWithTag() = opération qui coûte chère */
        if (collision.CompareTag("Player"))
            StartCoroutine(replacePlayer(collision));

    }

    /* Replacer le joueur avec une coroutine */
    public IEnumerator replacePlayer(Collider2D collision)
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        collision.transform.position = playerSpawn.position;
    }
}
