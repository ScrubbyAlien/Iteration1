using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Level : MonoBehaviour
{
    [SerializeField]
    private Grid grid;
    [SerializeField]
    private Tilemap levelFrame;
    [SerializeField, Tooltip("The size of each room by the number of tiles it is made up of in the x and y axis'")]
    private Vector2Int roomSize;
    [SerializeField, Tooltip("The coordinate of the room in the top left of the entire level")]
    private Vector2Int topLeftRoomCoord;
    [SerializeField, Tooltip("The coordinate of the room in the bottom cell of the entire level")]
    private Vector2Int bottomRightRoomCoord;
    [SerializeField, Tooltip("The coordinate of the cell in the top left of the starting room")]
    private Vector2Int startRoomCellCoord;
    [SerializeField, Tooltip("The coordinate of the cell in the top left of the exit room")]
    private Vector2Int exitRoomCellCoord;
    [SerializeField]
    private Room[] rooms;
    [SerializeField]
    private string exitScene;

    private void OnValidate() {
        foreach (Room room in rooms) {
            if (room.size.x != roomSize.x || room.size.y != roomSize.y) {
                Debug.LogWarning("One of the rooms in the available rooms does not match the size requirements");
                break;
            }
        }
    }

    private void Start() {
        GenerateLevel();
    }

    private void GenerateLevel() {
        List<Vector2Int> roomCoords = GenerateRoomCoords();

        // place a starting room
        Room startingRoom = GetRandomRoomOfType(Room.RoomType.Start);
        roomCoords.Remove(startRoomCellCoord);
        PlaceRoomAtCoord(startingRoom, startRoomCellCoord);

        // place exit room
        Room exitRoom = GetRandomRoomOfType(Room.RoomType.Exit);
        roomCoords.Remove(exitRoomCellCoord);
        PlaceRoomAtCoord(exitRoom, exitRoomCellCoord);

        // place remaining rooms
        foreach (Vector2Int coord in roomCoords) {
            PlaceRandomRoomAtCoord(coord);
        }
    }

    private Vector3 GetRoomOrigin(Vector2Int cellCoord) {
        Vector3 cellMidPoint = grid.GetCellCenterWorld((Vector3Int)cellCoord);
        return cellMidPoint - new Vector3(grid.cellSize.x / 4f, grid.cellSize.y / 4f);
    }

    private Room GetRandomRoom(ref Room[] rooms) {
        return rooms[Random.Range(0, rooms.Length)];
    }

    private Room GetRandomRoomOfType(Room.RoomType type) {
        Room[] roomsOfType = rooms.Where(r => r.roomType == type).ToArray();
        return roomsOfType[Random.Range(0, roomsOfType.Length)];
    }

    private bool GetNeighbourRoomCoord(Vector2Int roomCoord, Room.Exits exit, out Vector2Int neighbourCoord) {
        switch (exit) {
            case Room.Exits.North:
                neighbourCoord = roomCoord + new Vector2Int(0, roomSize.y);
                return neighbourCoord.y <= topLeftRoomCoord.y;
            case Room.Exits.East:
                neighbourCoord = roomCoord + new Vector2Int(roomSize.x, 0);
                return neighbourCoord.x <= bottomRightRoomCoord.x;
            case Room.Exits.South:
                neighbourCoord = roomCoord - new Vector2Int(0, roomSize.y);
                return neighbourCoord.y >= bottomRightRoomCoord.y;
            case Room.Exits.West:
                neighbourCoord = roomCoord - new Vector2Int(roomSize.x, 0);
                return neighbourCoord.x >= topLeftRoomCoord.x;
            default:
                neighbourCoord = new Vector2Int();
                return false;
        }
    }

    private List<Vector2Int> GenerateRoomCoords() {
        List<Vector2Int> coords = new();

        for (int x = topLeftRoomCoord.x; x <= bottomRightRoomCoord.x; x += roomSize.x) {
            for (int y = topLeftRoomCoord.y; y >= bottomRightRoomCoord.y; y -= roomSize.y) {
                coords.Add(new Vector2Int(x, y));
            }
        }
        return coords;
    }

    private void PlaceRandomRoomAtCoord(Vector2Int coord) {
        Room[] normalRooms = rooms.Where(r => r.roomType == Room.RoomType.Normal).ToArray();
        PlaceRoomAtCoord(GetRandomRoom(ref normalRooms), coord);
    }

    private void PlaceRoomAtCoord(Room room, Vector2Int coord) {
        room.tilemap.CompressBounds();
        for (int x = 0; x < room.size.x; x++) {
            for (int y = 0; y > -room.size.y; y--) {
                TileBase tile = room.tilemap.GetTile(new Vector3Int(x, y, 0));
                Vector3Int levelFrameCoord = (Vector3Int)coord + new Vector3Int(x, y, 0);
                levelFrame.SetTile(levelFrameCoord, tile);
            }
        }
    }
}