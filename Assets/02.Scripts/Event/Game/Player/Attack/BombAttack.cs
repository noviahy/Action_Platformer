using UnityEngine;
using static Player;

public class BombAttack : IAttackStratgy
{
    [SerializeField] private Player player;
    [SerializeField] PlayerAttack playerAttack;
    private float throwPower;

    private GameObject obj;
    private Rigidbody2D rb;

    public void Init(PlayerAttack attackCode, GameObject bombPrefab, float power)
    {
        playerAttack = attackCode;
        throwPower = power;
        obj = bombPrefab;
        rb = obj.GetComponent<Rigidbody2D>();
        rb.simulated = false;
    }
    public void Attack(EAttackType attackType)
    {
        obj.transform.SetParent(null);
        rb.simulated = true;

        if (attackType == EAttackType.PutBomb)
        {
            obj.transform.position = player.PutBombSoket.position;
            return;
        }
        if (attackType == EAttackType.Default)
        {
            Vector2 dir = (
         Vector2.right * player.Facing * 1f +
         Vector2.up * 0.7f
     ).normalized;
            rb.AddForce(dir * throwPower, ForceMode2D.Impulse);
        }
    }
}
