using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : Health
{
    [SerializeField]
    private HealthStats stats;
    [SerializeField]
    private string animatorDeadBoolName;
    [SerializeField]
    private float invincibilityTime;
    private Cooldown invincibilityCooldown;

    protected override void Start() {
        base.Start();
        stats.Initialize();
        stats.StatChanged += HealthChanged;
        damagePort.OnDamagePlayer += TakeDamage;
        invincibilityCooldown = new Cooldown(invincibilityTime);
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        stats.StatChanged -= HealthChanged;
        damagePort.OnDamagePlayer -= TakeDamage;
    }

    protected override void HealthChanged(int newValue) {
        if (newValue <= 0 && !dead) {
            Animator animator = GetComponent<Animator>();
            if (animator) animator.SetBool(animatorDeadBoolName, true);
            OnDie.Invoke();
            dead = true;
        }
    }

    public override void TakeDamage(int value) {
        if (!invincibilityCooldown.on) {
            stats.DealDamage(value);
            invincibilityCooldown.Start();
            OnTakeDamage?.Invoke();
        }
    }

    public override void Heal(int value) {
        stats.RecoverHealth(value);
    }

    public override void Kill() {
        stats.SetHealth(0);
    }

    public void ResetHealth() {
        stats.Reset();
    }
}