using UnityEngine;

public class Room : MonoBehaviour
{
    public Vector2Int size;
    public Exits[] exits;
    public RoomType roomType;

    public enum Exits
    {
        North,
        East,
        South,
        West
    }

    public enum RoomType
    {
        Start,
        Exit,
        Normal
    }
}