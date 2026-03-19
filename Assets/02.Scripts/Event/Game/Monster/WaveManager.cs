using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Wave[] waves;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Player player;
    [SerializeField] private Bar[] bars;

    private int aliveCount = 0;
    private int currentWave = 0;
    private bool waveStarted = false;
    private Coroutine coroutine;
    private void Update()
    {
        float diffX = Mathf.Abs(transform.position.x - player.transform.position.x);
        float diffY = Mathf.Abs(transform.position.y - player.transform.position.y);

        if (!waveStarted && diffX < 0.3f && diffY < 5f)
        {
            waveStarted = true;
            if (coroutine == null)
                coroutine = StartCoroutine(WaveDelay());
            foreach (Bar bar in bars)
                bar.RequestActive(true);
        }
    }
    void StartWave()
    {
        Wave wave = waves[currentWave];

        aliveCount = wave.spawnCount;

        int random = Random.Range(0, 6);

        for (int i = 0; i < wave.spawnCount; i++)
        {
            GameObject prefab = wave.monsterPrefabs[i % wave.monsterPrefabs.Length];

            Transform spawnPoint = spawnPoints[random + i];

            GameObject monster = Instantiate(
                prefab,
                spawnPoint.position,
                Quaternion.identity
            );
            monster.GetComponent<IMonster>().Init(this);
        }

        currentWave++;
    }

    public void OnMonsterDead()
    {
        aliveCount--;

        if (currentWave >= waves.Length && aliveCount <= 0)
        {
            foreach (Bar bar in bars)
                bar.RequestActive(false);
        }

        if (aliveCount <= 0 && currentWave < waves.Length)
        {
            if (coroutine == null)
                coroutine = StartCoroutine(WaveDelay());
        }
    }

    IEnumerator WaveDelay()
    {
        yield return new WaitForSeconds(1.5f);
        StartWave();
        coroutine = null;
    }
}
[System.Serializable]
public class Wave
{
    public GameObject[] monsterPrefabs;
    public int spawnCount;
}
