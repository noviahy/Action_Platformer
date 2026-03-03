using System.Collections;
using UnityEngine;

public class BossSword : MonoBehaviour, IBoss
{
    [SerializeField] private Player player;
    [SerializeField] private BossSwordTimer bossTimer;
    [SerializeField] private BossSwordPattern pattern;
    [SerializeField] private float rushDis;
    [SerializeField] private float slashDis;
    [SerializeField] private float addDis;
    [SerializeField] private GameObject activePoint;
    [SerializeField] private Bar bar;
    [SerializeField] private int BossHP;
    [SerializeField] private SpriteRenderer sprite;

    private Coroutine coroutine;
    private float diff;
    private float[] weights;

    public bool isActive { get; private set; } = false;
    public int moveX { get; private set; }
    public Vector2 rushPoint { get; private set; }

    private BossState lastState = BossState.Idle;
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
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        diff = player.transform.position.x - transform.position.x;
        moveX = diff > 0 ? 1 : -1;
        rushPoint = (Vector2)player.transform.position + Vector2.right * moveX * addDis;

        if (BossHP <= 0)
        {
            if (isActive)
            {
                isActive = false;
                RequestBarActive();
                StartCoroutine(StartDeadMotion());
            }
            return;
        }

        if (!isActive)
        {
            if (player.transform.position.x <= activePoint.transform.position.x)
            {
                isActive = true;
                coroutine = StartCoroutine(StartHowling());
                RequestBarActive();
            }
            else
                return;
        }
        if (coroutine != null)
            return;

        float t = Mathf.Clamp01(Mathf.Abs(diff) / 15f);
        weights[0] = Mathf.Lerp(20, 20, t); // °¡±î¿ò, ¸Ø
        weights[1] = Mathf.Lerp(10, 20, t);
        weights[2] = Mathf.Lerp(10, 20, t);
        weights[3] = Mathf.Lerp(0, 40, t);
        weights[4] = Mathf.Lerp(20, 30, t);
        weights[5] = Mathf.Lerp(20, 30, t);

        if (diff >= rushDis)
        {
            weights[4] = 70;
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
    private void RequestBarActive()
    {
        bar.RequestActive(isActive);
    }

    public void RequestDamage(int dmg)
    {
        BossHP -= dmg;
    }
    IEnumerator StartHowling()
    {
        yield return new WaitForSeconds(1.5f);
        coroutine = null;
    }
    IEnumerator StartDeadMotion()
    {
        float time = 0;
        float duration = 1.5f;

        while (time < duration)
        {

            time += Time.deltaTime;
            sprite.color = new Color(1, 1, 1, 1 - time / duration);
            yield return null;
        }
    }
}
