using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;

    [SerializeField]
    private float force;
    [SerializeField]
    private float maxVelocity;

    private float currentInput;

    private void Start() {
        body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        if (Mathf.Abs(currentInput) > 0) {
            Debug.Log(Vector2.right * (currentInput * force));
            body.AddForce(Vector2.right * (currentInput * force));
        }

        body.linearVelocity = Vector2.ClampMagnitude(body.linearVelocity, maxVelocity);
    }

    public void OnMove(InputValue value) {
        currentInput = value.Get<Vector2>().x;
    }
}