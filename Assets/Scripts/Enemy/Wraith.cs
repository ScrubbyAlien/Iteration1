using UnityEngine;

public class Wraith : Enemy
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float moveForce, maxVelocity;
    private int side = 1;

    private Transform target;
    private bool canSeeTarget;

    [SerializeField]
    private bool debug;

    private void Start() {
        animator.SetBool("walking", true);
    }

    private void FixedUpdate() {
        if (dead) return;

        side = Vector2.Dot(body.linearVelocity.normalized, Vector2.right) > 0 ? 1 : -1;
        spriteRenderer.flipX = side < 0;

        if (target && canSeeTarget) {
            Vector2 toTarget = (target.position - transform.position).normalized;
            body.AddForce(toTarget * moveForce);
        }

        if (body.linearVelocity.sqrMagnitude > maxVelocity * maxVelocity) {
            body.linearVelocity = Vector2.ClampMagnitude(body.linearVelocity, maxVelocity);
        }

        if (body.linearVelocity.sqrMagnitude > 0.1f * 0.1f) animator.SetBool("walking", true);
        else animator.SetBool("walking", false);
    }

    /// <inheritdoc />
    public override void Die() {
        base.Die();
        body.gravityScale = 1;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            target = other.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (target && other.gameObject == target.gameObject) {
            target = null;
        }
    }
}