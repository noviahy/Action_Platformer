using UnityEngine;
using static PlayerAttack;

public class BombAttack : IAttackStratgy
{
    private Player player;
    private float throwPower;

    private GameObject obj;
    private Rigidbody2D rbBomb;

    public void Init(Player playerCode,GameObject bombPrefab, float power)
    {
        player = playerCode;
        throwPower = power;
        obj = bombPrefab;
        rbBomb = obj.GetComponent<Rigidbody2D>();
        rbBomb.simulated = false;
    }
    public void Attack(EAttackType attackType)
    {
        obj.transform.SetParent(null);
        rbBomb.simulated = true;

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
            
            rbBomb.AddForce(dir * throwPower, ForceMode2D.Impulse);
        }
    }
}
