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
    private DestructionPort destructionPort;
    [SerializeField]
    private DamagePort damagePort;

    [SerializeField]
    private float fuseTime, damageRadius;

    [SerializeField]
    private Vector2Int[] affectedTerrainCoordsRelative;

    private float fuseFinishTime;

    private bool detonated;

    private void Start() {
        fuseFinishTime = Time.time + fuseTime;
    }

    private void Update() {
        if (Time.time >= fuseFinishTime && !detonated) {
            Detonate();
        }
    }

    private void Detonate() {
        detonated = true;
        destructionPort.DestroyTerrain(transform.position, affectedTerrainCoordsRelative);
        damagePort.CreateExplosion(transform.position, damageRadius, 1, gameObject);

        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            Detonate();
        }
    }
}