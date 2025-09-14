using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private DeathPort deathPort;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Vector3 playerStartPosition;
    [SerializeField, Min(0)]
    private float playerInputActiveDelay;

    public UnityEvent ResetLevel;
    public UnityEvent LevelStart;

    private PlayerInput playerInput;

    private void Awake() {
        player.transform.position = playerStartPosition;
        playerInput = player.GetComponent<PlayerInput>();
        playerInput.DeactivateInput();
        StartCoroutine(DelayInputActivation());
    }

    private void Start() {
        LevelStart?.Invoke();
        deathPort.OnPlayerDeath += InvokeResetLevel;
    }

    private void OnDestroy() {
        deathPort.OnPlayerDeath -= InvokeResetLevel;
    }

    private IEnumerator DelayInputActivation() {
        yield return new WaitForSecondsRealtime(playerInputActiveDelay);
        playerInput.ActivateInput();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(playerStartPosition, 0.05f);
    }

    private void InvokeResetLevel() {
        ResetLevel?.Invoke();
    }
}