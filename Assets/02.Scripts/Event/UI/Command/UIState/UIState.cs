using UnityEngine;

public abstract class UIState : MonoBehaviour
{
    public abstract EStateType StateType { get; }
    public virtual bool IsMenuState => false; // Stack
    public abstract void Enter();
    public abstract void Exit();
}
