using System.Collections;
using UnityEngine;
using static Player;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] bool isWalk;
    public float moveX { get; private set; }

    public bool isDash { get; private set; } = false;
    private bool isAttack = false;
    public void SetPlayer(Player newPlayer)
    {
        player = newPlayer;
    }
    private void Update()
    {
        float moveX = 0f;
        if (player == null) return;

        if (Input.GetKey(KeyCode.LeftArrow))
            moveX += -1f;

        if (Input.GetKey(KeyCode.RightArrow))
            moveX += 1f;

        if (moveX != 0f) // 딱히 지금은 의미가 없답니다~ 에니메이션 넣을때 쓸듯?
            player.AddFlags(PlayerStateFlags.Walk);

        // Desh
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (isDash) return;

            isDash = true;
            player.Dash();
            StartCoroutine(WaitForNextDesh());
            isDash = false;
        }
        // Attack
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!isAttack) return;

            isAttack = true;
            StartCoroutine(WaitForNextAttack());
            isAttack = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            if (isAttack) return;

            isAttack = true;
            StartCoroutine(WaitForNextAttack());
            isAttack = false;
        }
        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!groundCheck.IsGrounded) return;

            player.AddFlags(PlayerStateFlags.Jump);
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {

        }

    }
    IEnumerator WaitForNextDesh()
    {
        yield return new WaitForSeconds(2f);
    }
    IEnumerator WaitForNextAttack()
    {
        yield return new WaitForSeconds(0.5f);
        player.RemoveFlags(PlayerStateFlags.Attack);
    }
}
