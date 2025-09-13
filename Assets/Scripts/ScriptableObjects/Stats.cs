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
    protected bool initialized;

    public void Initialize() {
        if (initialized) return;
        InitializeStat();
        initialized = true;
    }

    public void Reset() {
        initialized = false;
        ResetStat();
    }
}