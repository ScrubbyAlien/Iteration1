using UnityEngine;

[RequireComponent(typeof(Grid)), ExecuteAlways]
public class GridCellVisualiser : MonoBehaviour
{
    [SerializeField]
    private Vector3Int cell;
    [SerializeField]
    private bool hide;

    private Grid grid;

    private void OnEnable() {
        grid = GetComponent<Grid>();
    }

    private void OnDrawGizmosSelected() {
        if (hide) return;
        Gizmos.DrawCube(grid.GetCellCenterWorld(cell), new Vector3(0.5f, 0.5f, 0.5f));
    }
}