using UnityEngine;
using static Player;

public class SwordAttack : MonoBehaviour, IAttackStratgy
{
    [SerializeField] private float defaultForce;
    [SerializeField] private float bombForce;
    private Collider2D hitBoxCol;
    private SpriteRenderer sprite;
    private Player player;
    private void Start()
    {
        hitBoxCol.enabled = false;
        sprite.enabled = false;
    }

    public void Init(GameObject gameObject, Player playerCode)
    {
        hitBoxCol = gameObject.GetComponent<Collider2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        player = playerCode;
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
                applyForce(other, defaultForce);
                break;
            case "Bomb":
                applyForce(other, bombForce);
                break;
            case "Boss":
                applyForce(other, 0);
                break;
        }
    }
    private void applyForce(Collider2D other, float force)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        Vector2 dir = (Vector2.right * player.Facing).normalized;

        rb.AddForce(dir * force, ForceMode2D.Impulse);
    }

}
