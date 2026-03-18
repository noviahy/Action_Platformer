using UnityEngine;
using static PlayerAttack;

public class SwordAttack : MonoBehaviour, IAttackStratgy
{
    [SerializeField] private float nockBackForce;
    [SerializeField] private float bombForce;
    [SerializeField] private Player player;
    private Collider2D hitBoxCol;
    private SpriteRenderer sprite;
    private Vector2 pivot;
    private void Start()
    {
        hitBoxCol.enabled = false;
        sprite.enabled = false;
    }
    public void Init(GameObject gameObject)
    {
        hitBoxCol = gameObject.GetComponent<Collider2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    public void Attack(EAttackType attackType)
    {
        hitBoxCol.enabled = true;
        sprite.enabled = true;
    }
    public void AttackFinish()
    {
        hitBoxCol.enabled = false;
        sprite.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag) 
        {
            case "Monster":
                var monster = other.GetComponentInParent<IMonster>();
                if (monster != null)
                {
                    pivot = player.transform.position;
                    monster.GetKnockbackInfo(pivot, nockBackForce);
                }
                break;
            case "Bomb":
                var bomb = other.GetComponent<Bomb>();
                pivot = player.transform.position;
                bomb.GetKnockbackInfo(pivot, bombForce);
                break;
        }
    }
}
