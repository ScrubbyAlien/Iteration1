using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameInfo gameInfo;
    [SerializeField]
    private DeathPort deathPort;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Vector3 playerStartPosition;
    [SerializeField, Min(0)]
    private float playerInputActiveDelay;

    public UnityEvent ResetGame;
    public UnityEvent NextLevel;
    public UnityEvent GameStart;

    private PlayerInput playerInput;

    private void Start() {
        // reset player position
        player.transform.position = playerStartPosition;
        playerInput = player.GetComponent<PlayerInput>();
        playerInput.DeactivateInput();
        StartCoroutine(DelayInputActivation());

        deathPort.OnPlayerDeath += InvokeResetGame;
        GameStart?.Invoke();
    }

    private void OnDestroy() {
        deathPort.OnPlayerDeath -= InvokeResetGame;
    }

    private IEnumerator DelayInputActivation() {
        yield return new WaitForSecondsRealtime(playerInputActiveDelay);
        playerInput.ActivateInput();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(playerStartPosition, 0.05f);
    }

    private void InvokeResetGame() {
        ResetGame?.Invoke();
        deathPort.OnPlayerDeath -= InvokeResetGame;
    }

    public void InvokeNextLevel() {
        NextLevel?.Invoke();
        deathPort.OnPlayerDeath -= InvokeResetGame;
    }
}