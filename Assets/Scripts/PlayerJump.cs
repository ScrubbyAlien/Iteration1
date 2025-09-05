using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using UnityEngine;

[RequireComponent(typeof(ContactHandler))]
public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D body;
    private ContactHandler contact;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float coyoteTime;
    [SerializeField, Min(1)]
    private float fallingGravityScale;

    // private Cooldown coyote;
    // private Cooldown jumpDelay;

    // private bool jumped;

    // private bool canJump => (contact.grounded || coyote.on) && !jumped;

    private Vector2 contactpoint;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        contact = GetComponent<ContactHandler>();
        // coyote = new Cooldown(coyoteTime);
        // jumpDelay = new Cooldown(0.1f);
    }

    void FixedUpdate() {
        // check coyoteTime

        if (body.linearVelocityY < -0.01f) body.gravityScale = fallingGravityScale;
        else body.gravityScale = 1;

        animator.SetBool("grounded", contact.grounded);
    }

    public void OnJump() {
        // maybe add sideways force if jumping while walking
        if (contact.grounded) {
            body.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            animator.SetTrigger("jump");
            // jumped = true;
            // jumpDelay.Start();
            // coyote.Stop();
        }
    }

    // private void OnDrawGizmos() {
    //     Gizmos.DrawLine(transform.position, contactpoint);
    // }
}