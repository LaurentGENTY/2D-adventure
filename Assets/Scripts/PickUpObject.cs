using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public AudioClip soundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlayClipAt(soundEffect, transform.position);

            /* Car on a un singleton sur l'inventaire */
            Inventory.instance.AddCoins(1);

            /* Ajoute dans le currentscenemanager afin de pouvoir gérer le cas de la mort du joueur et du coup retirer les pieces */
            CurrentSceneManager.instance.coinsPickedUpInThisSceneCount++;

            Destroy(gameObject);
        }
    }
}
