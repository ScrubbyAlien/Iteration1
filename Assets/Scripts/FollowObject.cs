using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField]
    private Transform follow;

    [SerializeField, Header("Follow properties")]
    private bool position, rotation, scale;

    void Start() { }

    private void LateUpdate() { }
}