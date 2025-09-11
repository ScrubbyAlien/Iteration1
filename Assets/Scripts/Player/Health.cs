using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent OnDie;

    [SerializeField]
    private HealthStats stats;

    private void Start() {
        stats.Initialize();
        stats.StatChanged += HealthChanged;
    }

    private void OnDestroy() {
        stats.StatChanged -= HealthChanged;
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
}