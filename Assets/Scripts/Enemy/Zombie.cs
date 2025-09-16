using UnityEngine;

public class Zombie : Enemy
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float moveSpeed;
    private float velocity;
    private int side = 1;

    private void Start() {
        velocity = moveSpeed;
        animator.SetBool("walking", true);
    }

    private void FixedUpdate() {
        if (dead) return;
        body.linearVelocityX = velocity * side;

        // check if about to walk into a wall, and turn around if so
        RaycastHit2D hitWall = Physics2D.Raycast(
            transform.position, Vector2.right * side, 0.2f,
            LayerMask.GetMask("Obstacle")
        );

        Vector3 groundRayOrigin = transform.position + Vector3.right * (side * 0.1f);
        RaycastHit2D hitGround = Physics2D.Raycast(
            groundRayOrigin, Vector2.down, 0.5f,
            LayerMask.GetMask("Obstacle")
        );

        if (hitWall.collider || !hitGround.collider) {
            side = side * -1;
            spriteRenderer.flipX = side < 0;
        }
    }

    /// <inheritdoc />
    public override void Die() {
        base.Die();
    }
}