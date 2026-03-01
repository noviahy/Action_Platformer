using UnityEngine;

public class BossCannon : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private BossCannonTimer bossTimer;
    [SerializeField] private BossCannonPattern pattern;
    [SerializeField] private GameObject activePoint;

    private float diff;
    private float[] weights;
    public bool isActive { get; private set; } = false;
    public int moveX { get; private set; }

    private BossState lastState = BossState.Idle;
    public enum BossState
    {
        Idle,
        Walk,
        Jump,
        DefaultAttack,
        GuidedAttack,
        PingPongAttack,
    }
    public BossState CurrentState { get; private set; }

    private void Start()
    {
        weights = new float[System.Enum.GetValues(typeof(BossState)).Length];
    }
    private void Update()
    {
        diff = player.transform.position.x - transform.position.x;
        moveX = diff > 0 ? 1 : -1;

        if (!isActive)
        {
            if (player.transform.position.x <= activePoint.transform.position.x)
                isActive = true;
            else
                return;
        }

        float t = Mathf.Clamp01(Mathf.Abs(diff) / 15f);
        weights[0] = Mathf.Lerp(20, 20, t); // °¡±î¿ò, ¸Ø
        weights[1] = Mathf.Lerp(10, 30, t);
        weights[2] = Mathf.Lerp(10, 20, t);
        weights[3] = Mathf.Lerp(30, 30, t);
        weights[4] = Mathf.Lerp(10, 40, t);
        weights[5] = Mathf.Lerp(30, 30, t);
    }
    private BossState GetRandomAction()
    {
        float total = 0;

        for (int i = 0; i < weights.Length; i++)
        {
            if ((BossState)i == lastState) continue;
            total += weights[i];
        }

        float random = Random.Range(0, total);

        float current = 0;

        for (int i = 0; i < weights.Length; i++)
        {
            if ((BossState)i == lastState) continue;

            current += weights[i];

            if (random <= current)
            {
                lastState = (BossState)i;
                return (BossState)i;
            }
        }
        return BossState.Idle;
    }
    public void GetNextPattern()
    {
        CurrentState = GetRandomAction();
        pattern.RequestAction(CurrentState);
    }
}
