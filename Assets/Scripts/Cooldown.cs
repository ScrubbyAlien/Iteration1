using System.Collections;
using UnityEngine;

public class Cooldown
{
    // private bool onCooldown = false;
    // private float cooldownLength;
    // private MonoBehaviour mono;
    // private Coroutine cooldownRoutine;
    //
    // public Cooldown(MonoBehaviour calling, float length) {
    //     mono = calling;
    //     cooldownLength = length;
    // }
    //
    // public void Start() {
    //     onCooldown = false;
    //     cooldownRoutine = mono.StartCoroutine(CooldownCounter());
    // }
    //
    // private IEnumerator CooldownCounter() {
    //     yield return new WaitForSeconds(cooldownLength);
    //     onCooldown = true;
    //     cooldownRoutine = null;
    // }
    //
    // public void Stop() {
    //     if (cooldownRoutine != null)
    //         mono.StopCoroutine(cooldownRoutine);
    //     cooldownRoutine = null;
    //     onCooldown = true;
    // }
    //
    // // convert to true if the cooldown is active and false if not
    // public static implicit operator bool(Cooldown cd) {
    //     return cd.onCooldown;
    // }

    private float cooldownLength;
    private float finishTime;
    private bool started;

    public Cooldown(float length) {
        cooldownLength = length;
    }

    public void Start() {
        if (started) return;
        started = true;
        finishTime = Time.time + cooldownLength;
    }

    public void Stop() {
        started = false;
        finishTime = 0;
    }

    public void Restart() {
        Stop();
        Start();
    }

    public bool on {
        get {
            started = Time.time < finishTime;
            return started;
        }
    }
}