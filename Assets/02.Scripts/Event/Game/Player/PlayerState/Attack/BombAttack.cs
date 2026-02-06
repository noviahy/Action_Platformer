using UnityEngine;
using static Player;

public class BombAttack : IAttackStratgy
{
    private Player player;
    private float throwPower;

    private GameObject obj;
    private Rigidbody2D rb;
    public void Init(Player playerCode, GameObject bombPrefab, float power)
    {
        player = playerCode;
        throwPower = power;
        obj = bombPrefab;
        rb = obj.GetComponent<Rigidbody2D>();
    }
    public void Attack(EAttackType attackType)
    {
        obj.transform.SetParent(null);

        if (attackType == EAttackType.PutBomb)
        {
            obj.transform.position = player.PutBombSoket.position;
            rb.simulated = false;
            return;
        }

        if (attackType == EAttackType.Default)
        {
            rb.simulated = true;

            Vector2 dir = (
         Vector2.right * player.Facing +
         Vector2.up * 0.5f
     ).normalized;
            rb.AddForce(dir * throwPower, ForceMode2D.Impulse);
        }
    }
}
