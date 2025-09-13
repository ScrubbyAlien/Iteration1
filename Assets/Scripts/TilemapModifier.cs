using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapModifier : MonoBehaviour
{
    [SerializeField]
    private Grid grid;
    [SerializeField]
    private Tilemap tilemap;

    [SerializeField]
    private DestructionPort destructionPort;

    public void Start() {
        destructionPort.OnDestroyTerrain += DestroyTiles;
    }

    public void OnDestroy() {
        destructionPort.OnDestroyTerrain -= DestroyTiles;
    }

    public void DestroyTiles(Vector3 origin, Vector2Int[] relativeCoords) {
        Vector3Int originCell = grid.WorldToCell(origin);
        foreach (Vector2Int coord in relativeCoords) {
            if (tilemap.GetTile<BreakableTile>(originCell + (Vector3Int)coord)) {
                tilemap.SetTile(originCell + (Vector3Int)coord, ScriptableObject.CreateInstance<Tile>());
            }
        }
    }
}