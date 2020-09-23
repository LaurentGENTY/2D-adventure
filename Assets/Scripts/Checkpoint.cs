using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public AudioClip checkpointSoundEffect;

    private Transform playerSpawn;

    public void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        /* Si on arrive au checkpoint, on desactive la boite de collision empechant l'utilisation d'anciens checkpoints */
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlayClipAt(checkpointSoundEffect, transform.position);

            playerSpawn.position = transform.position;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }  

    }

}
