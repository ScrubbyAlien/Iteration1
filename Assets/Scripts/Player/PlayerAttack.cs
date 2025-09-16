using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private PlayerMove move;

    [SerializeField]
    private Collider2D hitbox;
    private float baseXOffset;

    [SerializeField]
    private float attackCooldown;
    private Cooldown attack;
    [SerializeField]
    private int damage;

    public UnityEvent OnSwing;

    private void Start() {
        attack = new Cooldown(attackCooldown);
        baseXOffset = hitbox.offset.x;
        move.OnChangeSide += SwitchSide;
    }

    private void OnDestroy() {
        move.OnChangeSide -= SwitchSide;
    }

    private void SwitchSide(int newSide) {
        hitbox.offset = new Vector2(baseXOffset * newSide, hitbox.offset.y);
    }

    public void OnSlash() {
        if (!attack.on) {
            animator.SetTrigger("attack");
            attack.Start();
            OnSwing?.Invoke();
        }
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.isTrigger) return;
        Health health = other.GetComponent<Health>();
        if (health) health.TakeDamage(damage);
    }
}