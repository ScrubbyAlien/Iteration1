using Unity.Mathematics.Geometry;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private Transform launcher;
    private float baseXPos;
    [SerializeField]
    private float launchAngle, launchForce;
    [SerializeField]
    private Item spritBombPrefab;

    [SerializeField]
    private ItemStore spiritBombStore, moneyStore;

    [SerializeField]
    private PlayerMove move;
    private int currentSide = 1;

    private void Start() {
        baseXPos = launcher.localPosition.x;
        spiritBombStore.Initialize();
        move.OnChangeSide += SwitchSide;
    }

    private void OnDestroy() {
        move.OnChangeSide -= SwitchSide;
    }

    private void SwitchSide(int newSide) {
        currentSide = newSide;
        Vector3 lp = launcher.localPosition;
        launcher.localPosition = new Vector3(baseXPos * newSide, lp.y, lp.z);
    }

    public void OnBomb() {
        if (Physics2D.OverlapPoint(launcher.position, LayerMask.NameToLayer("Obstacle"))) return;
        if (spiritBombStore.Use()) {
            Item bomb = Instantiate(spritBombPrefab, launcher.position, Quaternion.identity);
            bomb.transform.position += Vector3.back;
            bomb.Launch(CalculateForceVector());
        }
    }
    private Vector2 CalculateForceVector() {
        float launchVectorX = Mathf.Cos(launchAngle * Mathf.Deg2Rad);
        float launchVectorY = Mathf.Sin(launchAngle * Mathf.Deg2Rad);
        return new Vector2(launchVectorX * currentSide, launchVectorY) * launchForce;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(launcher.position, 0.01f);
        Gizmos.DrawLine(launcher.position, launcher.position + (Vector3)CalculateForceVector().normalized);
    }

    public void ResetStore() {
        spiritBombStore.Reset();
        moneyStore.Reset();
    }
}