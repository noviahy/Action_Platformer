using UnityEngine;
using System.Collections;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] PlayerAttack attack;
    [SerializeField] PlayerKnockbackHandler knockbackHandler;
    [SerializeField] private float stopTime;
    private Coroutine coroutine;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BombItem"))
        {
            attack.GetBoom();
        }
        if (other.CompareTag("Sword"))
        {
            if (coroutine != null)
                return;
            coroutine = StartCoroutine(DoParrying());
        }
        if (other.CompareTag("Dead"))
        {
            player.RequestDead();
        }
    }

    IEnumerator DoParrying()
    {
        float prevScale = Time.timeScale;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(stopTime);

        Time.timeScale = prevScale;
        coroutine = null;
    }
}
