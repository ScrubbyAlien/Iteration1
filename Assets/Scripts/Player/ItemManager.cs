using Unity.Mathematics.Geometry;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private Transform launcher, launchGauge;
    private float baseXPos;
    [SerializeField]
    private float launchAngle, launchForce, minForce, maxForceTime;
    [SerializeField]
    private Item spritBombPrefab;

    [SerializeField]
    private ItemStore spiritBombStore;

    [SerializeField]
    private PlayerMove move;
    private int currentSide = 1;

    private bool holdingBomb;
    private float holdTime;

    private void Start() {
        baseXPos = launcher.localPosition.x;
        move.OnChangeSide += SwitchSide;
    }

    private void OnDestroy() {
        move.OnChangeSide -= SwitchSide;
    }

    private void Update() {
        Vector3 ls = launchGauge.localScale;
        if (holdingBomb && spiritBombStore.ReadStat() > 0) {
            holdTime += Time.deltaTime;
            launchGauge.localScale = new Vector3(CalculteGaugeScale(), ls.y, ls.z);
        }
        else {
            launchGauge.localScale = new Vector3(0, ls.y, ls.z);
        }
    }

    private float CalculteGaugeScale() {
        float cycle = holdTime % (maxForceTime * 2);
        float scale = (holdTime % maxForceTime) / maxForceTime;
        if (cycle >= maxForceTime) scale = 1 - scale;
        return scale;
    }

    private void SwitchSide(int newSide) {
        currentSide = newSide;
        Vector3 lp = launcher.localPosition;
        launcher.localPosition = new Vector3(baseXPos * newSide, lp.y, lp.z);
    }

    public void OnBomb(InputValue value) {
        if (value.isPressed) {
            holdingBomb = true;
            holdTime = 0;
        }
        else {
            holdingBomb = false;
            if (spiritBombStore.Use()) {
                Item bomb = Instantiate(spritBombPrefab, launcher.position, Quaternion.identity);
                bomb.transform.position += Vector3.back;
                bomb.Launch(CalculateForceVector());
            }
        }
    }
    private Vector2 CalculateForceVector() {
        float launchVectorX = Mathf.Cos(launchAngle * Mathf.Deg2Rad);
        float launchVectorY = Mathf.Sin(launchAngle * Mathf.Deg2Rad);
        return new Vector2(launchVectorX * currentSide, launchVectorY) *
               Mathf.Max(minForce, launchForce * CalculteGaugeScale());
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(launcher.position, 0.01f);
        Gizmos.DrawLine(launcher.position, launcher.position + (Vector3)CalculateForceVector().normalized);
    }
}