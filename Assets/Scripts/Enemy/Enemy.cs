using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody2D body;
    [SerializeField]
    protected Animator animator;
    [SerializeField]
    protected DamagePort damagePort;

    public bool dead { get; protected set; }

    public virtual void Die() {
        dead = true;
        animator.SetBool("dead", true);
    }

    protected virtual void OnCollisionEnter2D(Collision2D other) {
        if (dead) return;
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !other.collider.isTrigger) {
            damagePort.DamagePlayer(1);
        }
    }
}