using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Vector2 playerStartPosition;
    [SerializeField, Min(0)]
    private float playerInputActiveDelay;

    private PlayerInput playerInput;

    private void Start() {
        player.transform.position = playerStartPosition;
        playerInput = player.GetComponent<PlayerInput>();
        playerInput.DeactivateInput();
        StartCoroutine(DelayInputActivation());
    }

    private IEnumerator DelayInputActivation() {
        yield return new WaitForSecondsRealtime(playerInputActiveDelay);
        playerInput.ActivateInput();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(playerStartPosition, 0.05f);
    }
}