using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    public bool isInvincible = false;
    public SpriteRenderer graphics;
    public float secondsToWait = 0.2f;
    public float delayInvicibility = 1f;

    public static PlayerHealth instance;

    /* Gérer son */
    public AudioClip hitSoundEffect;

    /* Mettre en place le design pattern singleton */
    public void Awake()
    {
        if (instance)
            Debug.LogWarning("Déjà un playerHealth");

        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            TakeDamage(100);
    }

    public void HealPlayer(int amount)
    {
        if ((currentHealth += amount) > maxHealth)
            currentHealth = maxHealth;
        else
        {
            currentHealth += amount;
            healthBar.SetHealth(currentHealth);
        }
    }


    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            AudioManager.instance.PlayClipAt(hitSoundEffect, transform.position);

            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            /* Verifier que le joueur est toujours vivant, on meurt  */
            if (currentHealth <= 0)
            {
                Die();
                return;
            }

            isInvincible = true;
            /* Lancer la coroutine de flash */
            StartCoroutine(InvicibilityFlash());
            /* Lancer la coroutine du délai de flash */
            StartCoroutine(InvisibilityDelay());
        }

    }

    /* Quand le joueur est mort : on désactive son rigid body, son sprite ...*/
    public void Die()
    {
        /* On bloque les mouvements du personnage */
        PlayerMouvement.instance.enabled = false;
        
        /* Joue les animations */
        PlayerMouvement.instance.animator.SetTrigger("Die");

        /* Empecher le joueur d'intéragir avec les autres éléments */
        /* Le joueur ne bouge plus (plus sensible aux forces) */
        PlayerMouvement.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        /* Reset la force du mouvement */
        PlayerMouvement.instance.rb.velocity = Vector3.zero;

        PlayerMouvement.instance.playerCollider.enabled = false;

        /* Lance l'écran de game over */
        GameOverManager.instance.OnPlayerDeath();
    }

    /* On fait l'inverse de Die() */
    public void Respawn()
    {
        PlayerMouvement.instance.enabled = true;
        PlayerMouvement.instance.animator.SetTrigger("Respawn");
        PlayerMouvement.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        PlayerMouvement.instance.playerCollider.enabled = true;

        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }

    /* Ajout d'une coroutine permettant de réaliser une fonction toutes les X time */
    public IEnumerator InvicibilityFlash()
    {
        while (isInvincible)
        {
            /* Rendre le personnage transparent */
            graphics.color = new Color(1f, 1f, 1f, 0);
            yield return new WaitForSeconds(secondsToWait);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(secondsToWait);
        }
    }

    public IEnumerator InvisibilityDelay()
    {
        yield return new WaitForSeconds(delayInvicibility);
        isInvincible = false;
    }

}
