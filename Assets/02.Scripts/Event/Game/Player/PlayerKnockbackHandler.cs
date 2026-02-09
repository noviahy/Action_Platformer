using System.Collections;
using UnityEngine;

public class PlayerKnockbackHandler : MonoBehaviour, IKnockbackHandler
{
    [SerializeField] private float I_Time;
    [SerializeField] private float stopTime;
    [SerializeField] private Rigidbody2D rb;
    private SpriteRenderer bodySprite;
    private Vector2 nockBackdir;
    private float nockBackForce;

    private Coroutine coroutine;
    public bool isEnable { get; private set; } = true;
    public bool lockInput { get; private set; } = false;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        bodySprite = gameObject.GetComponent<SpriteRenderer>();
    }
    public void GetKnockbackInfo(Vector2 hitPoint, float force)
    {
        float dirX = transform.position.x - hitPoint.x > 0 ? 1f : -1f;
        float angle = 35f * Mathf.Deg2Rad;

        Vector2 dir = new Vector2(dirX * Mathf.Cos(angle), Mathf.Sin(angle)).normalized;

        nockBackdir = dir;
        nockBackForce = force;
        doNockBack();
    }

    private void doNockBack()
    {
        if (coroutine != null) return;

        coroutine = StartCoroutine(WaitForNockBack());
    }

    IEnumerator WaitForNockBack()
    {
        isEnable = false;
        lockInput = true;

        float prevScale = Time.timeScale;
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(stopTime);

        Time.timeScale = prevScale;
        rb.AddForce(nockBackdir * nockBackForce, ForceMode2D.Impulse);

        float elapsed = 0f;

        yield return new WaitForSeconds(0.5f);

        lockInput = false;
        while (elapsed < I_Time)
        {
            bodySprite.enabled = !bodySprite.enabled;
            yield return new WaitForSeconds(0.2f);
            elapsed += 0.2f;
        }
        isEnable = true;
        coroutine = null;
    }
}
