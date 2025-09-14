using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DeathPort", menuName = "Scriptable Objects/DeathPort")]
public class DeathPort : ScriptableObject
{
    public event Action OnPlayerDeath;

    public void TriggerPlayerDeath() {
        OnPlayerDeath?.Invoke();
    }
}