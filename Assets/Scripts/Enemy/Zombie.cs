using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D body;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private DamagePort damagePort;

    public void Die() {
        animator.SetBool("dead", true);
        body.simulated = false;
        body.GetComponent<Collider2D>().enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !other.collider.isTrigger) {
            damagePort.DamagePlayer(1);
        }
    }
}