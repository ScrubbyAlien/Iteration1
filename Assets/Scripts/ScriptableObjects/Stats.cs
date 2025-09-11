using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Scriptable Objects/Stats")]
public abstract class Stats<T> : ScriptableObject
{
    public event Action<T> StatChanged;
    public abstract T ReadStat();
    protected void InvokeStatChanged(T newValue) {
        StatChanged?.Invoke(newValue);
    }
}