﻿using UnityEngine;

public class SnakePatrol : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;

    private Transform target;

    public int dmgOnCollision = 20;
    private int destPoint = 0;

    public SpriteRenderer spriteRenderer;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        /* Vecteur3 pour aller vers la destination */
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * this.speed * Time.deltaTime, Space.World);

        /* Change de destination si le serpent est arrivé */
        if (Vector3.Distance(transform.position, target.position) < 0.03f)
        {
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /* Si on rentre en collision avec le joueur (taggé) on fait perdre de la vie au joueur */
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth health = collision.transform.GetComponent<PlayerHealth>();
            health.TakeDamage(dmgOnCollision);
        }
    }
}