using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public AudioClip checkpointSoundEffect;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        /* Si on arrive au checkpoint, on desactive la boite de collision empechant l'utilisation d'anciens checkpoints */
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlayClipAt(checkpointSoundEffect, transform.position);

            /* Changer la position du respawn à la position du checkpoint */
            CurrentSceneManager.instance.respawnPoint = transform.position;

            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }  

    }

}
