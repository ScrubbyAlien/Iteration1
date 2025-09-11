using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthStats", menuName = "Scriptable Objects/HealthStats")]
public class HealthStats : Stats<int>
{
    public event Action OnReachZeroHealth;

    [SerializeField, Min(1)]
    private int startingHealth;
    private int currentHealth;

    private bool initialized;
    private bool reachedZero;

    /// <inheritdoc />
    public override int ReadStat() {
        return currentHealth;
    }

    public void DealDamage(int value) {
        if (!initialized) Debug.LogWarning($"HealthStats object ({name}) has not been initialized");
        if (reachedZero) Debug.LogWarning($"HealthStats object ({name}) is at or below zero health");
        if (!initialized || reachedZero) return;

        currentHealth -= value;
        if (currentHealth <= 0) {
            OnReachZeroHealth.Invoke();
            reachedZero = true;
        }
    }

    public void RecoverHealth(int value, bool exceedMax = true) {
        currentHealth += value;
        if (!exceedMax) currentHealth = Mathf.Min(currentHealth, startingHealth);
    }

    public void Initialize() {
        currentHealth = startingHealth;
        initialized = true;
        reachedZero = false;
    }
}