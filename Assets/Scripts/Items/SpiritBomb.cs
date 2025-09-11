using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

    private Tilemap _tm;
    private Tilemap tilemap {
        get {
            if (_tm) return _tm;
            else {
                _tm = GameObject.FindGameObjectWithTag("MainTileMap").GetComponent<Tilemap>();
                return _tm;
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
        if (tilemap) {
            Vector3Int bombcell = tilemap.layoutGrid.WorldToCell(transform.position);
            // Debug.Log(destructableTilemap.GetTile<BreakableTile>(Vector3Int.zero));
            List<TileChangeData> cellsToExplode = GetTileChangeDatas(bombcell);
            tilemap.SetTiles(cellsToExplode.ToArray(), true);

            Health[] healthObjects = FindObjectsByType<Health>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            float radius = tilemap.layoutGrid.cellSize.x * 2.5f;
            foreach (Health health in healthObjects) {
                if ((health.transform.position - transform.position).sqrMagnitude < radius * radius) {
                    health.TakeDamage(1);
                }
            }
        }
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private List<TileChangeData> GetTileChangeDatas(Vector3Int origin) {
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

        List<TileChangeData> datas = new();
        for (int i = 0; i < 13; i++) {
            if (tilemap.GetTile(cells[i]) is BreakableTile) {
                datas.Add(new TileChangeData() { position = cells[i], tile = ScriptableObject.CreateInstance<Tile>() });
            }
        }
        return datas;
    }
}