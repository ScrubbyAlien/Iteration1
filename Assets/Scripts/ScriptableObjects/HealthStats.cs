using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthStats", menuName = "Scriptable Objects/HealthStats")]
public class HealthStats : Stats<int>
{
    [SerializeField, Min(1)]
    private int startingHealth;
    private int currentHealth;

    /// <inheritdoc />
    public override int ReadStat() {
        return currentHealth;
    }

    public void DealDamage(int value) {
        currentHealth -= value;
        InvokeStatChanged(currentHealth);
    }

    public void RecoverHealth(int value, bool exceedMax = true) {
        currentHealth += value;
        if (!exceedMax) currentHealth = Mathf.Min(currentHealth, startingHealth);
    }

    public void Initialize() {
        currentHealth = startingHealth;
    }
}