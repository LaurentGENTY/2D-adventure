﻿using UnityEngine;

public class WeakSpot : MonoBehaviour
{
    public GameObject toDestroy;

    public void Start()
    {
        toDestroy = transform.parent.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* Si on rentre en collision avec le joueur (taggé) on détruit le snake */
        if (collision.CompareTag("Player"))
            /* Attention : ici on est dans WeakSpot (boxcollider) ce qu'on veut delete c'est TOUT le snake (graphics, waypoints...) */
            Destroy(toDestroy);
    }
}
