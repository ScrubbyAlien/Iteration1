using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private Grid grid;

    [SerializeField, Tooltip("The size of each room by the number of tiles it is made up of in the x and y axis'")]
    private Vector2Int roomSize;

    [SerializeField, Tooltip("The coordinate of the cell in the top left of the entire level")]
    private Vector2Int topLeftCellCoord;

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
        // place starting room
        // place a snake of rooms until reach exit and place exit
        // fill the rest of the rooms. 
        
        
        
    }

    private Vector3 GetRoomOrigin(Vector3Int cellCoord) {
        Vector3 cellMidPoint = grid.GetCellCenterWorld(cellCoord);
        return cellMidPoint - new Vector3(grid.cellSize.x / 2f, grid.cellSize.y / 2f);
    }
}