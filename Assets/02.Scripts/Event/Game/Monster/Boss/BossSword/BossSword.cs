using UnityEngine;

public class BossSword : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private BossSwordAI bossAI;
    [SerializeField] private BossSwordPattern pattern;
    [SerializeField] private float rushDis;
    [SerializeField] private float slashDis;
    [SerializeField] private float addDis;
    [SerializeField] private GameObject activePoint;
    
    private float diff;
    private float[] weights;
    public bool isActive { get; private set; } = false;
    public int moveX {  get; private set; }
    public Vector2 rushPoint { get; private set; }
    public enum BossState
    {
        Idle,
        Walk,
        Jump,
        Rush,
        DefaultAttack,
        Slash,
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
        rushPoint = (Vector2)player.transform.position + Vector2.right * moveX * addDis;

        if (!isActive)
        {
            if (diff <= activePoint.transform.position.x)
                isActive = true;
            else
                return;
        }

        float t = Mathf.Clamp01(diff / 10f);
        weights[0] = Mathf.Lerp(40, 5, t);
        weights[1] = Mathf.Lerp(40, 5, t);
        weights[2] = Mathf.Lerp(40, 5, t);
        weights[3] = Mathf.Lerp(40, 5, t);
        weights[4] = Mathf.Lerp(40, 5, t);
        weights[5] = Mathf.Lerp(40, 0, t);

        if (diff >= rushDis)
        {

        }
        if (diff >= slashDis)
        {
            weights[3] = 0;
        }
        if (diff < slashDis)
        {
            weights[5] = 0;
        }
    }
    private BossState GetRandomAction()
    {
        float total = 0;

        for (int i = 0; i < weights.Length; i++)
            total += weights[i];

        float random = Random.Range(0, total);

        float current = 0;

        for (int i = 0; i < weights.Length; i++)
        {
            current += weights[i];

            if (random <= current)
                return (BossState)i;
        }

        return BossState.Idle;
    }
    public void GetNextPattern()
    {
        CurrentState = GetRandomAction();
        pattern.RequestAction(CurrentState);
    }

}
