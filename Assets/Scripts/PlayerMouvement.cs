using System;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    /* Faire bouger le player */
    public float moveSpeed;
    public float jumpForce;
    private Vector3 velocity = Vector3.zero;

    /* */
    public Rigidbody2D rb;

    private bool isJumping;
    private bool isGrounded;

    /* Variables gérant le saut : vérifier que l'on est bien en contact avec le sol sous les pieds */
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayer;

    /* Permet de gérer le flip et les animations des sprites */
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private float horizontalMouvement;

    public void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
            isJumping = true;
 
        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        /* Permettre le changement d'état dans l'animator selon la vitesse qu'on lui passe */
        animator.SetFloat("Speed", characterVelocity);
    }
     
    /* Opération de physique, la récupération des Input doit se faire dans Update */
    public void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);

        horizontalMouvement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        MovePlayer(horizontalMouvement);
    }

    private void MovePlayer(float _horizontalMouvement)
    {
        Vector3 targetVelocity = new Vector3(_horizontalMouvement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);

        if (isJumping) {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }

    public void Flip(float velocity)
    {
        if (velocity > 0.1f)
            spriteRenderer.flipX = false;
        else if (velocity < -0.1f)
            spriteRenderer.flipX = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
