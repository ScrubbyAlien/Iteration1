using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Vector3 playerStartPosition;
    [SerializeField, Min(0)]
    private float playerInputActiveDelay;
    [SerializeField]
    private bool resetItemStoresAndHealth;

    private PlayerInput playerInput;

    private void Awake() {
        player.transform.position = playerStartPosition;
        playerInput = player.GetComponent<PlayerInput>();
        playerInput.DeactivateInput();
        StartCoroutine(DelayInputActivation());

        if (resetItemStoresAndHealth) {
            player.GetComponent<ItemManager>().ResetStore();
            player.GetComponent<PlayerHealth>().ResetHealth();
        }
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