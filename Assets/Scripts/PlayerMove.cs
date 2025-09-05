using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ContactHandler))]
public class PlayerMove : MonoBehaviour
{
    private ContactHandler contact;
    private Rigidbody2D body;

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float force;
    [SerializeField]
    private float maxXVelocity, maxYVelocity;

    private float currentInput;

    private PlayerJump jump;

    // 1 = facing right, -1 = facing left;
    private int side = 1;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        jump = GetComponent<PlayerJump>();
        contact = GetComponent<ContactHandler>();
    }

    void FixedUpdate() {
        //check if pushing against wall
        float correctedInput = currentInput;
        if (contact.touchingLeft) correctedInput = Mathf.Max(0, currentInput);
        if (contact.touchingRight) correctedInput = Mathf.Min(0, currentInput);

        body.AddForce(Vector2.right * (correctedInput * force));

        body.linearVelocityX = Mathf.Clamp(body.linearVelocityX, -maxXVelocity, maxXVelocity);
        body.linearVelocityY = Mathf.Clamp(body.linearVelocityY, -maxYVelocity, maxYVelocity);

        animator.SetBool("running", Mathf.Abs(body.linearVelocityX) > 0.05f);

        // update which side the player is facing
        if (currentInput != 0) side = currentInput > 0 ? 1 : -1;
        if (spriteRenderer) spriteRenderer.flipX = side < 0;
    }

    public void OnMove(InputValue value) {
        currentInput = value.Get<Vector2>().x;
    }
}