using UnityEngine;
using static Player;

public class BombAttack : MonoBehaviour, IAttackStratgy
{
    [SerializeField] GameObject Bomb;
    [SerializeField] Player player;
    [SerializeField] float throwPower;

    private GameObject obj;
    private Rigidbody2D rb;
    private void Start()
    {
        obj = Instantiate(
        Bomb,
        player.BombSoket.position,
        Quaternion.identity,
        player.BombSoket
    );
        rb = obj.GetComponent<Rigidbody2D>();
    }
    public void Attack(EAttackType attackType)
    {
        Bomb.transform.SetParent(null);

        if (attackType == EAttackType.PutBomb)
        {
            obj.transform.position = player.PutBombSoket.position;
            rb.simulated = false;
            return;
        }

        if (attackType == EAttackType.ThrowBomb)
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
