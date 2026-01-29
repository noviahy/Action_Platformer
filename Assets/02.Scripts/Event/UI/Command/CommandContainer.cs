using System.Collections.Generic;
using UnityEngine;

public class CommandContainer : MonoBehaviour
{
    [SerializeField] private List<IUIState> states;

    public Dictionary<EStateType, IUIState> commandDict;

    private void Awake()
    {
        initalize();
    }

    private void initalize()
    {
        commandDict = new Dictionary<EStateType, IUIState>();
    }
}
