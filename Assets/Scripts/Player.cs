using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoves : MonoBehaviour
{
    public float runSpeed = 2;
    public float jumpSpeed = 3;
    public float doubleJumpSpeed = 2.5f;

    Rigidbody2D rb2D;

    private bool canDoubleJump;
    private bool canWallJump; // Nueva variable para permitir el salto en la pared

    public bool betterJump = false;
    public float fallMultiplier = 0.5f;
    public float lowJumpMultiplier = 1f;

    public CheckGround checkGrounds;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public float dashCooldown;
    public float dashForce = 30;
    public GameObject dashParticle;

    bool isTouchingFront = false;
    bool wallSliding;
    Vector2 wallSlideDirection;
    public float wallSlideSpeed = 0.75f;
    bool isTouchingDerecha;
    bool isTouchingIzquierda;

    public float wallJumpXFactor = 1.5f;
    public float wallJumpYFactor = 1.5f;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        dashCooldown -= Time.deltaTime;

        if (Input.GetKey("w"))
        {
            if (checkGrounds.isGrounded)
            {
                canDoubleJump = true;
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
            }
            else if (canWallJump) // Verifica si se puede hacer un salto en la pared
            {
                WallJump();
            }
            else
            {
                if (Input.GetKeyDown("w"))
                {
                    if (canDoubleJump)
                    {
                        animator.SetBool("DoubleJump", true);
                        rb2D.velocity = new Vector2(rb2D.velocity.x, doubleJumpSpeed);
                        canDoubleJump = false;
                    }
                }
            }
        }

        if (isTouchingFront == true && checkGrounds.isGrounded == false)
        {
            wallSliding = true;
            wallSlideDirection = isTouchingDerecha ? Vector2.left : Vector2.right;
            canWallJump = true; // Activa la habilidad de saltar en la pared
        }
        else
        {
            wallSliding = false;
            canWallJump = false; // Desactiva la habilidad de saltar en la pared
        }

        if (wallSliding)
        {
            WallSlide();
        }
    }
    void FixedUpdate()
    {
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            if (isTouchingDerecha == false)
            {
                rb2D.velocity = new Vector2(runSpeed, rb2D.velocity.y);
                spriteRenderer.flipX = false;
                animator.SetBool("Run", true);
            }
            else
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, -wallSlidingSpeed);
            }
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            if (isTouchingIzquierda == false)
            {
                rb2D.velocity = new Vector2(-runSpeed, rb2D.velocity.y);
                spriteRenderer.flipX = true;
                animator.SetBool("Run", true);
            }
            else
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, -wallSlidingSpeed);
            }
        }
        else
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            animator.SetBool("Run", false);
        }

        if (Input.GetKey("s") && dashCooldown <= 0)
        {
            Dash();
        }

        if (betterJump)
        {
            if (rb2D.velocity.y < 0)
            {
                rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
            }
            else if (rb2D.velocity.y > 0 && !Input.GetKey("w"))
            {
                rb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
            }
        }
    }

    public void Dash()
    {
        GameObject dashObject;
        dashObject = Instantiate(dashParticle, transform.position, transform.rotation);

        if (spriteRenderer.flipX == true)
        {
            rb2D.AddForce(Vector2.left * dashForce, ForceMode2D.Impulse);
        }
        else
        {
            rb2D.AddForce(Vector2.right * dashForce, ForceMode2D.Impulse);
        }

        dashCooldown = 2;

        Destroy(dashObject, 1);
    }

private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ParedDerecha"))
        {
            isTouchingFront = true;
            isTouchingDerecha = true;
        }
        if (collision.gameObject.CompareTag("ParedIzquierda"))
        {
            isTouchingFront = true;
            isTouchingIzquierda = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isTouchingFront = false;
        isTouchingDerecha = false;
        isTouchingIzquierda = false;
    }

    void WallSlide()
    {
        rb2D.velocity = new Vector2(wallSlideDirection.x * runSpeed, -wallSlideSpeed);
    }

    private void WallJump()
    {
        Vector2 jumpDirection = isTouchingDerecha ? Vector2.left : Vector2.right;
        rb2D.velocity = new Vector2(jumpDirection.x * wallJumpXFactor, wallJumpYFactor);
        canWallJump = false;
    }
}