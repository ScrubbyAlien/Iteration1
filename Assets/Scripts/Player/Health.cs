using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent OnDie;

    [SerializeField]
    private HealthStats stats;
    [SerializeField]
    private DamagePort damagePort;

    private void Start() {
        stats.Initialize();
        stats.StatChanged += HealthChanged;
        damagePort.OnExplosion += OnExplosion;
    }

    private void OnDestroy() {
        stats.StatChanged -= HealthChanged;
        damagePort.OnExplosion -= OnExplosion;
    }

    private void HealthChanged(int newValue) {
        if (newValue <= 0) OnDie.Invoke();
    }

    public void TakeDamage(int value) {
        stats.DealDamage(value);
    }

    public void Heal(int value) {
        stats.RecoverHealth(value);
    }

    private void OnExplosion(Vector3 origin, float radius, int damage, GameObject _) {
        if ((transform.position - origin).sqrMagnitude <= radius) {
            TakeDamage(damage);
        }
    }
}