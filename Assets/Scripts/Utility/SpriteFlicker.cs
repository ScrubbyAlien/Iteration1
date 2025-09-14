using UnityEngine;

public class SpriteFlicker : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private float flicksPerSecond;
    private float timeTillNextFlick => 1 / flicksPerSecond;

    private Cooldown flickerCooldown;
    private float nextFlickTime;

    private void Start() {
        flickerCooldown = new Cooldown(0);
    }

    private void Update() {
        if (flickerCooldown.on) {
            if (Time.time >= nextFlickTime) {
                spriteRenderer.enabled = !spriteRenderer.enabled;
                nextFlickTime = Time.time + timeTillNextFlick;
            }
        }
        else {
            spriteRenderer.enabled = true;
        }
    }

    public void StartFlicker(float duration) {
        flickerCooldown = new Cooldown(duration);
        flickerCooldown.Start();
        nextFlickTime = Time.time + timeTillNextFlick;
    }
}