using UnityEngine;
using static PlayerAttack;

public class SwordAttack : MonoBehaviour, IAttackStratgy
{
    [SerializeField] private float nockBackForce;
    [SerializeField] private float bombForce;
    private Collider2D hitBoxCol;
    private SpriteRenderer sprite;
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
                var monster = other.GetComponent<IMonster>();
                monster.GetKnockbackInfo(transform.position, nockBackForce);
                break;
            case "Bomb":
                var bomb = other.GetComponent<Bomb>();
                bomb.GetKnockbackInfo(transform.position, bombForce);
                break;
                /*
            case "Boss":
                var Boss = other.GetComponent<IBoss>();
                monster.GetKnockbackInfo(transform.position, nockBackForce);
                break;
                */
        }
    }
}
