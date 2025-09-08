using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    
    [SerializeField]
    private float force;
    [SerializeField]
    private float maxXVelocity, maxYVelocity;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float coyoteTime;

    private float coyoteTimeEnd;

    // internal bools;
    private bool onGround;
    private bool inCoyoteTime;

    private float currentInput;

    private void Start() {
        body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        if (Mathf.Abs(currentInput) > 0) {
            //TODO: check if pushing against wall

            body.AddForce(Vector2.right * (currentInput * force));
        }

        body.linearVelocityX = Mathf.Clamp(body.linearVelocityX, -maxXVelocity, maxXVelocity);
        body.linearVelocityY = Mathf.Clamp(body.linearVelocityY, -maxYVelocity, maxYVelocity);

        // check if on ground
        List<ContactPoint2D> points = new();
        if (body.GetContacts(points) > 0) {
            foreach (ContactPoint2D point in points) {
                Vector2 centerToPoint = point.point - (Vector2)transform.position;
                if (Vector2.Angle(centerToPoint, Vector2.down) < 45f) {
                    onGround = true;
                }
            }
        }
        else {
            // if (!)
            SetUnGrounded();
        }
    }

    public void OnMove(InputValue value) {
        currentInput = value.Get<Vector2>().x;
    }

    public void OnJump() {
        // maybe add sideways force if jumping while walking
        if (onGround) body.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    private IEnumerator SetUnGrounded() {
        if (onGround) {
            coyoteTimeEnd = Time.time + coyoteTime;
            while (Time.time < coyoteTimeEnd) {
                yield return null;
            }
        }
    }

    // private void OnDrawGizmos() {
    //     Gizmos.DrawLine(transform.position, transform.position + Vector3.down);
    // }
}