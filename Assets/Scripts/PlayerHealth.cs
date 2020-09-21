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
    public float delayInvicibility = 3f;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            TakeDamage(20);
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            isInvincible = !isInvincible;
            /* Lancer la coroutine de flash */
            StartCoroutine(InvicibilityFlash());
            /* Lancer la coroutine du délai de flash */
            StartCoroutine(InvisibilityDelay());
        }

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
        isInvincible = !isInvincible;
    }
}
