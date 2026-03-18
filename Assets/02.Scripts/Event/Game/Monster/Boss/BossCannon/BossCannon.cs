using UnityEngine;
using System.Collections;
public class BossCannon : MonoBehaviour, IBoss
{
    [SerializeField] private Player player;
    [SerializeField] private BossCannonTimer bossTimer;
    [SerializeField] private BossCannonPattern pattern;
    [SerializeField] private GameObject activePoint;
    [SerializeField] private int BossHP;
    [SerializeField] private Bar[] bars;
    [SerializeField] private Transform attackRoot;

    private Coroutine coroutine;
    private SpriteRenderer sprite;
    private float diff;
    private float[] weights;
    public bool isActive { get; private set; } = false;
    public int moveX { get; private set; } = 1;

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
        pattern.Init(player);
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        diff = player.transform.position.x - transform.position.x;
        Vector3 scale = attackRoot.localScale;
        scale.x = Mathf.Abs(scale.x) * moveX;
        attackRoot.localScale = scale;

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
            if (Mathf.Abs(player.transform.position.x - activePoint.transform.position.x) <= 0.3f)
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
        SetMoveX();
        CurrentState = GetRandomAction();
        pattern.RequestAction(CurrentState);
    }
    public void RequestDamage(int dmg)
    {
        BossHP -= dmg;
    }
    private void SetMoveX()
    {
        moveX = diff > 0 ? 1 : -1;
    }
    private void RequestBarActive()
    {
        foreach (Bar bar in bars)
            bar.RequestActive(isActive);
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
        gameObject.SetActive(false);
    }
    IEnumerator StartHowling()
    {
        yield return new WaitForSeconds(1.5f);
        GetNextPattern();
        coroutine = null;
    }
}
