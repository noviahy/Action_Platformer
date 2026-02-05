using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform BombSoket;
    public Transform PutBombSoket;
    
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerTrigger playerTrigger;
    [SerializeField] GameObject playerGameObject;
    private IAttackStratgy currentAttack;
    private IAttackStratgy defaultAttack;
    public int Facing { get; private set; } = 1;
    public Vector3 PlayerLocation { get; private set; }
    public enum EAttackType
    {
        ThrowBomb,
        PutBomb,
        Sword
    }
    private void Start()
    {
        PlayerLocation = playerGameObject.transform.position;
        Collider2D col = playerGameObject.GetComponent<Collider2D>();
        Vector2 topPos = col.bounds.max;

    }
    private void FixedUpdate()
    {
        
    }
    public void Walk(int facing)
    {
        Facing = facing;
    }

    public void Jump()
    {

    }

    public void Attack(EAttackType attackType)
    {
        currentAttack.Attack(attackType);
        if (currentAttack != defaultAttack) 
        {
            currentAttack = defaultAttack;
        }
    }

    public void GetBoom()
    {
        currentAttack = new BombAttack();
    }
    private void OnTriggerEnter(Collider other)
    {
            playerTrigger.CollisionPlayer(other);
    }
}
