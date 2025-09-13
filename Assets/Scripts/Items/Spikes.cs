using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        Health impaled = other.GetComponent<Health>();
        if (impaled) {
            impaled.Kill();
        }
    }
}