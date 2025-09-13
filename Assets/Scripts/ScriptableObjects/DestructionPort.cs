using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "DestructionPort", menuName = "Scriptable Objects/DestructionPort")]
public class DestructionPort : ScriptableObject
{
    /// <summary>
    /// Methods subscribed to this event will be called when objects try to destory destructible terrain
    /// </summary>
    /// <param name="Vector3">The origin of the destructor</param>
    /// <param name="Vector2Int[]">A list of relative coordinates denoting which tiles are affected by the destruction</param>
    public event Action<Vector3, Vector2Int[]> OnDestroyTerrain;

    public void DestroyTerrain(Vector3 origin, Vector2Int[] coords) {
        OnDestroyTerrain?.Invoke(origin, coords);
    }
}