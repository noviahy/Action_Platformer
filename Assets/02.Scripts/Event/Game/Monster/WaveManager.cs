using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Wave[] waves;
    [SerializeField] private Transform[] spawnPoints;

    private int aliveCount = 0;
    private int currentWave = 0;

    private void Start()
    {
        StartWave();
    }

    void StartWave()
    {
        if (currentWave >= waves.Length)
            return;

        Wave wave = waves[currentWave];
        aliveCount = 0;

        for (int i = 0; i < wave.spawnCount; i++)
        {
            GameObject prefab = wave.monsterPrefabs[i % wave.monsterPrefabs.Length];

            Transform spawnPoint = spawnPoints[i % spawnPoints.Length];

            GameObject monster = Instantiate(
                prefab,
                spawnPoint.position,
                Quaternion.identity
            );
            aliveCount++;
            monster.GetComponent<IMonster>().Init(this);
        }

        currentWave++;
    }

    public void OnMonsterDead()
    {
        aliveCount--;

        if (aliveCount <= 0)
        {
            StartWave();
        }
    }
}
[System.Serializable]
public class Wave
{
    public GameObject[] monsterPrefabs;
    public int spawnCount;
}
