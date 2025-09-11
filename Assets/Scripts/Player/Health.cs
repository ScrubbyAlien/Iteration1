using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent OnDie;

    [SerializeField]
    private HealthStats stats;

    private void Start() {
        stats.Initialize();
        stats.OnReachZeroHealth += AtZeroHealth;
    }

    private void OnDestroy() {
        stats.OnReachZeroHealth -= AtZeroHealth;
    }

    private void AtZeroHealth() {
        OnDie.Invoke();
    }

    public void TakeDamage(int value) {
        stats.DealDamage(value);
    }

    public void Heal(int value) {
        stats.RecoverHealth(value);
    }
}