using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class NormalMonster : MonoBehaviour
{
    [SerializeField] int monsterHP;
    [SerializeField] float walkSpeed;
    [SerializeField] float nockBackTime;
    [SerializeField] int moveX;
    private Rigidbody2D rb;
    private bool isAttacked = false;
    private GameManager gameManager;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameManager.Instance;
    }
    private void FixedUpdate()
    {
        if(gameManager.CurrentState != GameManager.GameState.Playing)
            return;

        if (monsterHP <= 0)
        {
            gameObject.SetActive(false);
        }
        
        if (isAttacked) return;
        Walk();
    }

    private void Walk()
    {
        rb.linearVelocity = new Vector2(moveX * walkSpeed, rb.linearVelocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sword") || other.CompareTag("Player"))
        {
            --monsterHP;
            StartCoroutine(KnockBackTime());
        }

        if (other.CompareTag("Explosion"))
        {
            isAttacked = true;
            monsterHP -= 2;
            StartCoroutine(KnockBackTime());
        }
    }

    IEnumerator KnockBackTime()
    {
        yield return new WaitForSeconds(nockBackTime);
        isAttacked = false;
    }
}
