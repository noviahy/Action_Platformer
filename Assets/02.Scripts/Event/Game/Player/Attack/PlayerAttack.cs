using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public Transform PutBombSoket;
    [SerializeField] private Transform BombSoket;

    [SerializeField] private float throwForce;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private SwordAttack swordAttack;
    [SerializeField] private BombAttack bombAttack;
    [SerializeField] private Player player;

    private GameObject currentBomb;
    private IAttackStratgy currentAttack;
    private IAttackStratgy defaultAttack;
    public EAttackType AttackType { get; private set; }
    public enum EAttackType
    {
        Default,
        PutBomb
    }
    private void Start()
    {
        AttackType = EAttackType.Default;
    }

    public void GetBoom()
    {
        if (currentBomb != null) return;

        currentBomb = Instantiate(
        bombPrefab,
        BombSoket.position,
        Quaternion.identity,
        BombSoket);

        bombAttack.Init(this, currentBomb, throwForce);

        currentAttack = bombAttack;
    }

    private void attack()
    {
        if (AttackType == EAttackType.PutBomb && BombSoket.childCount == 0)
        {
            currentBomb = null;
            currentAttack = defaultAttack;
            AttackType = EAttackType.Default;
            return;
        }

        if (BombSoket.childCount == 0)
        {
            currentAttack = defaultAttack;
        }

        // currentAttack.Attack(AttackType);

        // putBomb
        if (currentAttack != defaultAttack)
        {
            currentAttack = defaultAttack;
            AttackType = EAttackType.Default;
        }
        // throw
        if (currentBomb != null)
        {
            currentAttack = defaultAttack;
            currentBomb = null;
        }
    }
    IEnumerator WaitForNextAttack()
    {
        yield return new WaitForSeconds(0.3f);
        swordAttack.AttackFinish();
        // player.lockAttack = false;
    }
}
