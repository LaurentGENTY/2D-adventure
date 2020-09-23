using System;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    /* Faire bouger le player */
    public float moveSpeed;
    public float climbSpeed;
    public float jumpForce;
    private Vector3 velocity = Vector3.zero;

    /* */
    public Rigidbody2D rb;

    private bool isJumping;
    private bool isGrounded;
    [HideInInspector]
    public bool isClimbing;

    /* Variables gérant le saut : vérifier que l'on est bien en contact avec le sol sous les pieds */
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayer;

    /* Permet de gérer le flip et les animations des sprites */
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public CapsuleCollider2D playerCollider;

    private float horizontalMouvement;
    private float verticalMouvement;

    public AudioClip jumpSoundEffect;

    /* Singleton */
    public static PlayerMouvement instance;

    public void Awake()
    {
        if (instance)
            Debug.LogWarning("Déjà un script playerMouvement");

        instance = this;
    }
    public void Update()
    {
        /* Récupérer le mouvement vertical et horizontal */
        horizontalMouvement = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;
        verticalMouvement = Input.GetAxis("Vertical") * climbSpeed * Time.fixedDeltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded && !isClimbing)
        {
            AudioManager.instance.PlayClipAt(jumpSoundEffect, transform.position);
            isJumping = true;
        }

        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);

        /* Permettre le changement d'état dans l'animator selon l'état du déplacement du joueur */
        animator.SetFloat("Speed", characterVelocity);
        animator.SetBool("isClimbing", isClimbing);
    }
     
    /* Opération de physique, la récupération des Input doit se faire dans Update */
    public void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);
        MovePlayer(horizontalMouvement,verticalMouvement);
    }

    private void MovePlayer(float _horizontalMouvement, float _verticalMouvement)
    {
        if (!isClimbing)
        {
            /* Déplacement horizontal */
            Vector3 targetVelocity = new Vector2(_horizontalMouvement, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);

            if (isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce));
                isJumping = false;
            }
        } else
        {
            /* Déplacement vertical */
            Vector3 targetVelocity = new Vector2(0, _verticalMouvement);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);
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
