using Unity.VisualScripting;
using UnityEngine;

public class Wraith : Enemy
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private BoxCollider2D boxCollider;

    [SerializeField]
    private float moveForce, maxVelocity;
    private int side = 1;

    private Transform target;
    private bool canSeeTarget;

    private bool deadOnTheGround;

    private void FixedUpdate() {
        if (dead) {
            if (deadOnTheGround) return;

            float rayLength = boxCollider.bounds.extents.y + boxCollider.edgeRadius + 0.01f;
            RaycastHit2D hitGround = Physics2D.Raycast(
                transform.position, Vector2.down, rayLength,
                LayerMask.GetMask("Obstacle")
            );
            if (hitGround) {
                deadOnTheGround = true;
            }
            return;
        }

        side = Vector2.Dot(body.linearVelocity.normalized, Vector2.right) >= 0 ? 1 : -1;
        spriteRenderer.flipX = side < 0;

        if (target) {
            RaycastHit2D obscured = Physics2D.Linecast(
                transform.position, target.position,
                LayerMask.GetMask("Obstacle")
            );

            canSeeTarget = !obscured.collider;
        }

        if (target && canSeeTarget) {
            Vector2 toTarget = (target.position - transform.position).normalized;
            body.AddForce(toTarget * moveForce);
        }

        body.linearVelocity = Vector2.ClampMagnitude(body.linearVelocity, maxVelocity);

        if (body.linearVelocity.sqrMagnitude > 0.1f * 0.1f) animator.SetBool("walking", true);
        else animator.SetBool("walking", false);
    }

    /// <inheritdoc />
    public override void Die() {
        base.Die();
        body.gravityScale = 1;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (target || dead) return;
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            target = other.gameObject.transform;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (target || dead) return;
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            target = other.gameObject.transform;
        }
    }
}