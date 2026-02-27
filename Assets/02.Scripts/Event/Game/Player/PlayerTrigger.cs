using UnityEngine;
using System.Collections;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] PlayerAttack player;
    [SerializeField] PlayerKnockbackHandler knockbackHandler;
    [SerializeField] private float stopTime;
    private Coroutine coroutine;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BombItem"))
        {
            player.GetBoom();
        }
        if (other.CompareTag("Sword"))
        {
            if (coroutine != null)
                return;
            coroutine = StartCoroutine(DoParrying());
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
