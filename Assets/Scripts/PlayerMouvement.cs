using System;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    public Rigidbody2D rb;

    private bool isJumping;
    private bool isGrounded;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);

        float horizontalMouvement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded)
            isJumping = true;

        MovePlayer(horizontalMouvement);

        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
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
}
