using UnityEngine;
using UnityEngine.Events;

public abstract class Health : MonoBehaviour
{
    public UnityEvent OnDie;
    public bool dead { protected set; get; }
    public UnityEvent OnTakeDamage;

    [SerializeField]
    protected DamagePort damagePort;

    protected virtual void Start() {
        damagePort.OnExplosion += OnExplosion;
    }

    protected virtual void OnDestroy() {
        damagePort.OnExplosion -= OnExplosion;
    }

    protected virtual void HealthChanged(int newValue) {
        if (newValue <= 0 && !dead) {
            OnDie.Invoke();
            dead = true;
        }
    }

    public abstract void TakeDamage(int value);

    public abstract void Heal(int value);

    public abstract void Kill();

    protected virtual void OnExplosion(Vector3 origin, float radius, int damage, GameObject _) {
        if (((Vector2)transform.position - (Vector2)origin).sqrMagnitude <= radius * radius) {
            TakeDamage(damage);
        }
    }
}