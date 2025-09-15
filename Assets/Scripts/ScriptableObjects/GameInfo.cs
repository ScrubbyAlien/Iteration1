using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "GameInfo", menuName = "Scriptable Objects/GameInfo")]
public class GameInfo : ScriptableObject
{
    public bool gameStarted { get; private set; }
    private bool initializationOpen = true;

    [SerializeField]
    private Stats<int>[] intStats;

    public void InitializeGame() {
        if (!initializationOpen) return;
        foreach (Stats<int> intStat in intStats) {
            intStat.Initialize();
        }
        initializationOpen = false;
        Application.quitting += () => initializationOpen = true;
#if UNITY_EDITOR
        EditorApplication.playModeStateChanged += (PlayModeStateChange stateChange) => {
            if (stateChange == PlayModeStateChange.ExitingPlayMode) initializationOpen = true;
        };
#endif
    }

    public void OpenInitialization() {
        initializationOpen = true;
    }
}