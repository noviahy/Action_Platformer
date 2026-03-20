using UnityEngine;
using System.Collections;
public class LavaPool : MonoBehaviour
{
    private float deceleration = 0.5f;
    private float damageInterval = 1f;
    private float timer = 0f;
    private CoinHPManager coinHPManager;
    private void Start()
    {
        coinHPManager = CoinHPManager.Instance;
        StartCoroutine(CountDown());
    }
    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(2.1f);
        Destroy(gameObject);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<Player>();
            player.RequestSetDeceleration(deceleration);
            timer += Time.deltaTime;

            if(timer >= damageInterval)
            {
                coinHPManager.Damage();
                timer = 0f;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<Player>();
            player.RequestNoDeceleration();
        }
        timer = 0f;
    }

}
