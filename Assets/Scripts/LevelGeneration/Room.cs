using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    public Tilemap tilemap;
    public Vector2Int size;
    public Exits[] exits;
    public RoomType roomType;

    private List<Exits> availableExits;

    private void Awake() {
        availableExits = new();
        foreach (Exits exit in exits) {
            availableExits.Add(exit);
        }
    }

    public Exits GetNextExit() {
        Exits nextExit = availableExits[Random.Range(0, availableExits.Count)];
        availableExits.Remove(nextExit);
        // if (nextExit != null) 
        return nextExit;
    }

    public enum Exits
    {
        North,
        East,
        South,
        West,
    }

    public enum RoomType
    {
        Start,
        Exit,
        Normal
    }
}