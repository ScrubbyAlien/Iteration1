using System.Linq;
using UnityEngine;

public class ContactHandler : MonoBehaviour
{
    [SerializeField]
    private float skinDepth;
    [SerializeField]
    private float rayLength;

    public bool grounded { get; private set; }
    public bool touchingLeft { get; private set; }
    public bool touchingRight { get; private set; }
    // public bool halfGrounded { get; private set; }

    [SerializeField]
    private Collider2D col;
    [SerializeField]
    private LayerMask collisionLayers;

    private Vector2 downOrigin;
    private Vector2 downLeftOrigin;
    private Vector2 downRightOrigin;

    private Vector2 leftOrigin;
    private Vector2 leftTopOrigin;
    private Vector2 leftBottomOrigin;

    private Vector2 rightOrigin;
    private Vector2 rightTopOrigin;
    private Vector2 rightBottomOrigin;

    private Vector2 rightCornerOrigin;
    private Vector2 leftCornerOrigin;

    // Update is called once per frame
    void FixedUpdate() {
        CalculateRayOrigins();

        grounded = false;
        touchingLeft = false;
        touchingRight = false;

        RaycastHit2D groundHit1 = Physics2D.Raycast(downLeftOrigin, Vector2.down, rayLength, collisionLayers);
        RaycastHit2D groundHit2 = Physics2D.Raycast(downOrigin, Vector2.down, rayLength, collisionLayers);
        RaycastHit2D groundHit3 = Physics2D.Raycast(downRightOrigin, Vector2.down, rayLength, collisionLayers);

        RaycastHit2D leftHit1 = Physics2D.Raycast(leftTopOrigin, Vector2.left, rayLength, collisionLayers);
        RaycastHit2D leftHit2 = Physics2D.Raycast(leftOrigin, Vector2.left, rayLength, collisionLayers);
        RaycastHit2D leftHit3 = Physics2D.Raycast(leftBottomOrigin, Vector2.left, rayLength, collisionLayers);

        RaycastHit2D rightHit1 = Physics2D.Raycast(rightTopOrigin, Vector2.right, rayLength, collisionLayers);
        RaycastHit2D rightHit2 = Physics2D.Raycast(rightOrigin, Vector2.right, rayLength, collisionLayers);
        RaycastHit2D rightHit3 = Physics2D.Raycast(rightBottomOrigin, Vector2.right, rayLength, collisionLayers);

        // Vector2 downRight = (Vector2.right + Vector2.down).normalized;
        // Vector2 downLeft = (Vector2.left + Vector2.down).normalized;
        // RaycastHit2D rightCorner = Physics2D.Raycast(rightCornerOrigin, downRight, rayLength, collisionLayers);
        // RaycastHit2D leftCorner = Physics2D.Raycast(leftCornerOrigin, downLeft, rayLength, collisionLayers);

        grounded = AnyColliders(groundHit1, groundHit2, groundHit3);
        // halfGrounded = AnyColliders(rightCorner, leftCorner);
        touchingLeft = AnyColliders(leftHit1, leftHit2, leftHit3);
        touchingRight = AnyColliders(rightHit1, rightHit2, rightHit3);
    }

    // private void OnDrawGizmosSelected() {
    //     CalculateRayOrigins();
    //
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawLine(downOrigin, downOrigin + Vector2.down * rayLength);
    //     Gizmos.DrawLine(leftOrigin, leftOrigin + Vector2.left * rayLength);
    //     Gizmos.DrawLine(rightOrigin, rightOrigin + Vector2.right * rayLength);
    // }

    private void CalculateRayOrigins() {
        float downOriginY = col.bounds.center.y - col.bounds.extents.y + skinDepth;
        float leftOriginX = col.bounds.center.x - col.bounds.extents.x + skinDepth;
        float rightOriginX = col.bounds.center.x + col.bounds.extents.x - skinDepth;

        downRightOrigin = new Vector2(col.bounds.max.x - skinDepth, downOriginY);
        downOrigin = new Vector2(col.bounds.center.x, downOriginY);
        downLeftOrigin = new Vector2(col.bounds.min.x + skinDepth, downOriginY);

        leftTopOrigin = new Vector2(leftOriginX, col.bounds.max.y - skinDepth);
        leftOrigin = new Vector2(leftOriginX, col.bounds.center.y);
        leftBottomOrigin = new Vector2(leftOriginX, col.bounds.min.y + skinDepth);

        rightTopOrigin = new Vector2(rightOriginX, col.bounds.max.y - skinDepth);
        rightOrigin = new Vector2(rightOriginX, col.bounds.center.y);
        rightBottomOrigin = new Vector2(rightOriginX, col.bounds.min.y + skinDepth);

        rightCornerOrigin = new Vector2(rightOriginX, downOriginY);
        leftCornerOrigin = new Vector2(leftOriginX, downOriginY);
    }

    private bool AnyColliders(params RaycastHit2D[] hits) {
        return hits.Where(h => h.collider).Any();
    }
}