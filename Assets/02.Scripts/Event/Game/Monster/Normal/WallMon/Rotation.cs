using UnityEngine;
using System.Collections;


public class Rotation : MonoBehaviour
{
    [SerializeField] GameObject firePrefab;
    [SerializeField] GameObject fireBallSoket;
    [SerializeField] Player player;
    [SerializeField] float activeDis;

    private GameManager gameManager;
    private Vector2 shootDir;
    private Coroutine coroutine;
    public bool isActive { get; private set; } = false;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        if (gameManager.CurrentState != GameManager.GameState.Playing)
            return;

        shootDir = player.transform.position - transform.position;

        float distSqr = shootDir.sqrMagnitude;

        isActive = distSqr < activeDis * activeDis;
    }

    public void followPlayer()
    {
        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg + 180f;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        float rotationSpeed = 30f;

        transform.rotation = Quaternion.RotateTowards(
    transform.rotation,
    targetRotation,
    rotationSpeed * Time.deltaTime
);
    } 
    public void RequestCoroutine()
    {
        if (coroutine != null)
            return;
        coroutine = StartCoroutine(StartAttack());
    }
    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 3; i++)
        {
            GameObject fireBall = Instantiate(firePrefab, fireBallSoket.transform.position, Quaternion.identity);

            FireBall fireBallCode = fireBall.GetComponent<FireBall>();
            fireBallCode.Init(shootDir.normalized, player);
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(3f);
        coroutine = null;
    }

}
