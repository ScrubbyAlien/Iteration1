using System;
using UnityEngine;

public abstract class Stats<T> : ScriptableObject
{
    public event Action<T> StatChanged;
    public abstract T ReadStat();
    protected void InvokeStatChanged(T newValue) {
        StatChanged?.Invoke(newValue);
    }

    protected virtual void InitializeStat() { }
    protected virtual void ResetStat() { }

    public void Initialize() {
        InitializeStat();
    }
}