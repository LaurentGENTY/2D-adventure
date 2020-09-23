using UnityEngine;

public class HealPowerUp : MonoBehaviour
{
    public int amount;

    public AudioClip soundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PlayerHealth.instance.currentHealth != PlayerHealth.instance.maxHealth)
        {
            AudioManager.instance.PlayClipAt(soundEffect, transform.position);

            /* Ajouter de la vie */
            PlayerHealth.instance.HealPlayer(amount);

            /* Supprimer la vie sur le level  */
            Destroy(gameObject);
        }
    }
}
