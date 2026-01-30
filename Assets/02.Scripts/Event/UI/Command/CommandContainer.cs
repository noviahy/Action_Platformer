using System.Collections.Generic;
using UnityEngine;

public class CommandContainer : MonoBehaviour
{
    [SerializeField] private List<StatePair> states;

    public Dictionary<EStateType, UIState> commandDict;

    private void Awake()
    {
        Initialize();
    }
    private void Initialize()
    {
        commandDict = new Dictionary<EStateType, UIState>();
        foreach (var pair in states) {
            commandDict[pair.State] = pair.UIClass;
        }
    }
}
[System.Serializable]
public class StatePair
{
    public EStateType State;
    public UIState UIClass;
}