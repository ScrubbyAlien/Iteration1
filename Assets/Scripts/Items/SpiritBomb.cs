using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpiritBomb : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem explosion;

    [SerializeField]
    private float fuseTime;

    private float fuseFinishTime;

    private bool detonated;

    private Tilemap _dtmp;
    private Tilemap destructableTilemap {
        get {
            if (_dtmp) return _dtmp;
            else {
                _dtmp = GameObject.FindObjectsByType<Tilemap>(FindObjectsSortMode.None)
                                  .Where(o => o.gameObject.tag == "Destructable").First();
                return _dtmp;
            }
        }
    }

    private void Start() {
        fuseFinishTime = Time.time + fuseTime;
    }

    private void Update() {
        if (Time.time >= fuseFinishTime && !detonated) {
            detonated = true;
            Detonate();
        }
    }

    private void Detonate() {
        if (destructableTilemap) {
            Vector3Int bombcell = destructableTilemap.layoutGrid.WorldToCell(transform.position);
            // Debug.Log(destructableTilemap.GetTile<BreakableTile>(Vector3Int.zero));
            TileChangeData[] cellsToExplode = GetTileChangeDatas(bombcell);
            destructableTilemap.SetTiles(cellsToExplode, true);
        }
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private TileChangeData[] GetTileChangeDatas(Vector3Int origin) {
        Vector3Int[] cells = new Vector3Int[13];
        cells[0] = origin; // 0 0
        cells[1] = new Vector3Int(origin.x, origin.y + 1); // 0 1
        cells[2] = new Vector3Int(origin.x + 1, origin.y + 1); // 1 1
        cells[3] = new Vector3Int(origin.x + 1, origin.y); // 1 0
        cells[4] = new Vector3Int(origin.x + 1, origin.y - 1); // 1 -1
        cells[5] = new Vector3Int(origin.x, origin.y - 1); // 0 -1
        cells[6] = new Vector3Int(origin.x - 1, origin.y - 1); // -1 -1
        cells[7] = new Vector3Int(origin.x - 1, origin.y); // -1 0
        cells[8] = new Vector3Int(origin.x - 1, origin.y + 1); // -1 1
        cells[9] = new Vector3Int(origin.x, origin.y + 2); // 0 2
        cells[10] = new Vector3Int(origin.x, origin.y - 2); // 0 -2
        cells[11] = new Vector3Int(origin.x + 2, origin.y); // 2 0
        cells[12] = new Vector3Int(origin.x - 2, origin.y); // -2 0

        TileChangeData[] datas = new TileChangeData[13];
        for (int i = 0; i < 13; i++) {
            datas[i] = new TileChangeData() { position = cells[i], tile = ScriptableObject.CreateInstance<Tile>() };
        }
        return datas;
    }
}