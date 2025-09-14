using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    private ItemStore itemStore;

    [SerializeField]
    private int amount;

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            itemStore.Get(amount);
            Destroy(gameObject);
        }
    }
}