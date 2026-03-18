using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject cannonBallPrefab;
    [SerializeField] GameObject spawnSoket;
    [SerializeField] float fireInterval;
    [SerializeField] float nextStartInterval;
    [SerializeField] float activeDis;
    [SerializeField] ParticleSystem explodeParticel;
    [SerializeField] int moveX;
    private GameManager gameManager;
    private Vector2 diff;
    private Coroutine coroutine;
    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        if (gameManager.CurrentState != GameManager.GameState.Playing)
            return;

        diff = player.transform.position - transform.position;

        if (diff.sqrMagnitude < activeDis * activeDis && coroutine == null)
        {
            coroutine = StartCoroutine(SpawnCannonBall());
        }
    }
    public void RequestDestroy()
    {
        Instantiate(explodeParticel, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
    IEnumerator SpawnCannonBall()
    {
        for (int i = 0; i < 3; i++)
        {
            var ball = Instantiate(cannonBallPrefab, spawnSoket.transform.position, Quaternion.identity);
            CannonBall ballCode = ball.GetComponent<CannonBall>();
            ballCode.Init(player, moveX);
            yield return new WaitForSeconds(fireInterval);
        }
        yield return new WaitForSeconds(nextStartInterval);
        coroutine = null;
    }
}
