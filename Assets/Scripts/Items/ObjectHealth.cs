using UnityEngine;

public class ObjectHealth : Health
{
    [SerializeField]
    private int startHealth;
    private int currentHealth;

    /// <inheritdoc />
    protected override void Start() {
        base.Start();
        currentHealth = startHealth;
    }

    /// <inheritdoc />
    protected override void OnDestroy() {
        base.OnDestroy();
    }

    public override void TakeDamage(int value) {
        currentHealth -= value;
        HealthChanged(currentHealth);
    }

    public override void Heal(int value) {
        currentHealth += value;
        HealthChanged(currentHealth);
    }

    public override void Kill() {
        currentHealth = 0;
        HealthChanged(currentHealth);
    }
}