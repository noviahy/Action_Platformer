using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Player player;
    public static InputManager Instance { get; private set; }
    public float moveX { get; private set; }
    private void Awake()
    {
        
    }
    private void Start()
    {
        Instance = this;
    }

    public void SetPlayer(Player newPlayer)
    {
        player = newPlayer;
    }
    private void Update()
    {
        moveX = 0f;
        if (player == null) return;

        if (Keyboard.current.leftArrowKey.isPressed)
            moveX += -1f;

        if (Keyboard.current.rightArrowKey.isPressed)
            moveX += 1f;

         // Desh
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame)
        {
            // Debug.Log("Dash Input");
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
            player.RequestAttack();
        }
        // Jump
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            // Debug.Log("Jump Input");
            player.RequestJump();
        }


        if (Keyboard.current.escapeKey.isPressed)
        {

        }

    }

}
