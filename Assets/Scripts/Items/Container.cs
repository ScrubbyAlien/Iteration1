using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Container : MonoBehaviour
{
    [SerializeField]
    private float pickupExitForce;
    [SerializeField]
    private Drop[] drops;

    public void Break() {
        if (drops.Length == 0) return;

        Drop drop = drops[Random.Range(0, drops.Length)];
        int numberOfDrops = Random.Range(drop.minDrops, drop.maxDrops);
        for (int i = 0; i < numberOfDrops; i++) {
            Rigidbody2D body = Instantiate(drop.pickup, transform.position, Quaternion.identity)
                .GetComponent<Rigidbody2D>();
            body.AddForce(GetRandomExitVector() * pickupExitForce, ForceMode2D.Impulse);
            body.AddTorque(Random.Range(-pickupExitForce, pickupExitForce));
        }

        Destroy(gameObject);
    }

    private Vector2 GetRandomExitVector() {
        Vector2 random = Random.insideUnitCircle;
        random = new Vector2(random.x, Mathf.Abs(random.y));
        return random;
    }
}

[Serializable]
public struct Drop
{
    public Pickup pickup;
    [Min(1)]
    public int minDrops;
    [Min(1)]
    public int maxDrops;
}