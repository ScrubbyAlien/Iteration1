using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Animator))]
public class PlayerBlink : MonoBehaviour
{
    [SerializeField, Min(0)]
    private float minBlinkWait;
    [SerializeField, Min(1)]
    private float maxBlinkWait;

    private float nextBlinkTime;
    private Animator playerAnimator;

    private void OnValidate() {
        if (maxBlinkWait < minBlinkWait) maxBlinkWait = minBlinkWait;
    }

    private void Start() {
        playerAnimator = GetComponent<Animator>();
        NewBlinkTime();
    }

    // Update is called once per frame
    void Update() {
        if (Time.time >= nextBlinkTime) {
            playerAnimator.SetTrigger("Blink");
            NewBlinkTime();
        }
    }

    private void NewBlinkTime() {
        Assert.IsTrue(maxBlinkWait >= minBlinkWait);
        nextBlinkTime = Time.time + Random.Range(minBlinkWait, maxBlinkWait);
    }
}