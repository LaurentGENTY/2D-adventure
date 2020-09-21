﻿using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            /* Car on a un singleton sur l'inventaire */
            Inventory.instance.AddCoins(1);
            Destroy(gameObject);
        }
    }
}
