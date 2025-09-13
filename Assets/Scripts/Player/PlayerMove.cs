using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ContactHandler))]
public class PlayerMove : MonoBehaviour
{
    public event Action<int> OnChangeSide;

    private ContactHandler contact;
    private Rigidbody2D body;
    private PlayerJump jump;

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float walkForce, sprintForce;
    private float force => sprinting ? sprintForce : walkForce;
    [SerializeField]
    private float maxXVelocityWalk, maxXVelocitySprint, maxYVelocity;
    private float maxXVelocity {
        get {
            if (maxVelocityLocked) return lockedMaxVelocity;
            else return sprinting ? maxXVelocitySprint : maxXVelocityWalk;
        }
    }
    private bool maxVelocityLocked;
    private float lockedMaxVelocity;
    [SerializeField, Range(0, 1)]
    private float stoppingFactor;

    private float currentInput;
    private float correctedInput;
    private bool sprinting => holdingSprint && Mathf.Abs(correctedInput) > 0;
    private bool holdingSprint;

    // 1 = facing right, -1 = facing left;
    private int side = 1;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        contact = GetComponent<ContactHandler>();
        jump = GetComponent<PlayerJump>();
        jump.OnJumpStart += LockMaxVelocity;
        jump.OnJumpLand += UnlockMaxVelocity;
    }

    private void OnDestroy() {
        jump.OnJumpStart -= LockMaxVelocity;
        jump.OnJumpLand -= UnlockMaxVelocity;
    }

    void FixedUpdate() {
        //check if pushing against wall
        correctedInput = currentInput;
        if (contact.touchingLeft) correctedInput = Mathf.Max(0, currentInput);
        if (contact.touchingRight) correctedInput = Mathf.Min(0, currentInput);

        body.AddForce(Vector2.right * (correctedInput * force));

        body.linearVelocityX = Mathf.Clamp(body.linearVelocityX, -maxXVelocity, maxXVelocity);
        body.linearVelocityY = Mathf.Clamp(body.linearVelocityY, -maxYVelocity, maxYVelocity);

        if (contact.grounded && correctedInput == 0 && !jump.jumped) body.linearVelocity *= stoppingFactor;

        animator.SetBool("running", Mathf.Abs(currentInput) > 0 && sprinting);
        animator.SetBool("walking", Mathf.Abs(currentInput) > 0 && !sprinting);

        // update which side the player is facing
        if (currentInput != 0) {
            int newSide = currentInput > 0 ? 1 : -1;
            if (side != newSide) OnChangeSide?.Invoke(newSide);
            side = newSide;
        }
        if (spriteRenderer) spriteRenderer.flipX = side < 0;
    }

    public void OnMove(InputValue value) {
        currentInput = value.Get<Vector2>().x;
    }

    public void OnSprint(InputValue value) {
        holdingSprint = value.Get<float>() > 0;
    }

    private void LockMaxVelocity() {
        lockedMaxVelocity = maxXVelocity;
        maxVelocityLocked = true;
    }

    private void UnlockMaxVelocity() {
        maxVelocityLocked = false;
    }
}