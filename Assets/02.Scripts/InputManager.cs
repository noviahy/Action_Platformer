using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameManager gameManager;
    public static InputManager Instance { get; private set; }
    public float moveX { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public void SetPlayer(Player newPlayer)
    {
        player = newPlayer;
    }
    private void Update()
    {
        if (gameManager.CurrentState != GameManager.GameState.Playing)
            return;

        moveX = 0f;
        if (player == null) return;

        if (Keyboard.current.leftArrowKey.isPressed)
            moveX += -1f;

        if (Keyboard.current.rightArrowKey.isPressed)
            moveX += 1f;
        // Desh
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame)
        {
            player.RequestDash();
        }
        // Attack
        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            player.RequestAttack();
        }
        if (Keyboard.current.leftCtrlKey.wasPressedThisFrame)
        {
            player.ChangeAttackType();
        }
        // Jump
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            player.RequestJump();
        }
        // Back
        if (Keyboard.current.escapeKey.isPressed)
        {
            uiManager.GoBackState();
        }
    }
}
