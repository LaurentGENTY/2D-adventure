using UnityEngine;

public class SnakePatrol : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;

    private Transform target;

    private int destPoint = 0;

    public SpriteRenderer spriteRenderer;


    void Start()
    {
        target = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        /* Vecteur3 pour aller vers la destination */
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * this.speed * Time.deltaTime, Space.World);

        /* Change de destination si le serpent est arrivé */ 
        if (Vector3.Distance(transform.position, target.position) < 0.03f) {
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }
}
