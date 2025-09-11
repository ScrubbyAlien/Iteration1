using System.Xml;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Scriptable Objects/Stats")]
public abstract class Stats<T> : ScriptableObject
{
    public abstract T ReadStat();
}
