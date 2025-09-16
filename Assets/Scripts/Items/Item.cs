using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Item : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D body;

    public void Launch(Vector2 force) {
        body.AddForce(force, ForceMode2D.Impulse);
    }

    private void Start() { }

    private void Update() { }
}