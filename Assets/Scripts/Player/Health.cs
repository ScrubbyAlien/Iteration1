using UnityEngine;
using UnityEngine.Events;

public abstract class Health : MonoBehaviour
{
    public UnityEvent OnDie;

    [SerializeField]
    protected DamagePort damagePort;

    protected virtual void Start() {
        damagePort.OnExplosion += OnExplosion;
    }

    protected virtual void OnDestroy() {
        damagePort.OnExplosion -= OnExplosion;
    }

    protected virtual void HealthChanged(int newValue) {
        if (newValue <= 0) {
            OnDie.Invoke();
        }
    }

    public abstract void TakeDamage(int value);

    public abstract void Heal(int value);

    public abstract void Kill();

    protected virtual void OnExplosion(Vector3 origin, float radius, int damage, GameObject _) {
        if ((transform.position - origin).sqrMagnitude <= radius) {
            TakeDamage(damage);
        }
    }
}