using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DeathPort", menuName = "Scriptable Objects/DeathPort")]
public class DeathPort : ScriptableObject
{
    public event Action OnPlayerDeath;
    public bool playerDead { get; private set; }

    public void TriggerPlayerDeath() {
        playerDead = true;
        OnPlayerDeath?.Invoke();
    }
}