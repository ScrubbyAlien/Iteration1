using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DamagePort", menuName = "Scriptable Objects/DamagePort")]
public class DamagePort : ScriptableObject
{
    /// <summary>
    /// Methods subscribed to this event will be called when something explodes in the scene.
    /// </summary>
    /// <param name="Vector3">Origin of the explosion</param>
    /// <param name="float">Radius</param>
    /// <param name="int">Damage</param>
    /// <param name="GameObject">The game object that created the explosion</param>
    public event Action<Vector3, float, int, GameObject> OnExplosion;

    [SerializeField]
    private float cellSize;

    /// <summary>
    /// Create an explosion at origin with radius in tiles
    /// </summary>
    public void CreateExplosion(Vector3 origin, float radius, int damage, GameObject originator) {
        OnExplosion?.Invoke(origin, radius * cellSize, damage, originator);
    }
}