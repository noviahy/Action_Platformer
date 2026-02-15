using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform BombSoket;

    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject swordObject;

    [SerializeField] private SwordAttack swordAttack;
    [SerializeField] private BombAttack bombAttack;
    [SerializeField] private Player player;

    [SerializeField] private float throwForce;

    private GameObject currentBomb;
    private Coroutine coroutine;
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
        bombAttack = new BombAttack();
        AttackType = EAttackType.Default;
    }
    private void OnEnable()
    {
        swordAttack.Init(swordObject);
        defaultAttack = swordAttack;
        currentAttack = defaultAttack;
    }
    public void ChangeAttackType()
    {
        if (currentBomb == null) return;
        AttackType = EAttackType.PutBomb;
        player.RequestAttack();
    }
    public void GetBoom()
    {
        if (currentBomb != null) return;

        currentBomb = Instantiate(
        bombPrefab,
        BombSoket.position,
        Quaternion.identity,
        BombSoket);

        bombAttack.Init(player, currentBomb, throwForce);

        currentAttack = bombAttack;
    }

    public void attack()
    {
        if (coroutine != null)
            return;

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

        RequestAttackRoutine();

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
    public void RequestAttackRoutine()
    {
        if (coroutine != null) return;

        currentAttack.Attack(AttackType);
        coroutine = StartCoroutine(WaitForNextAttack());
    }
    IEnumerator WaitForNextAttack()
    {
        yield return new WaitForSeconds(0.3f);
        swordAttack.AttackFinish();
        coroutine = null;
    }
}
