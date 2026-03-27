using UnityEngine;
using System.Collections;
public class LavaPool : MonoBehaviour
{
    private float deceleration = 0.3f;
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
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponentInParent<Player>();
            player.RequestSetDeceleration(deceleration);
            timer += Time.deltaTime;

            if(timer >= damageInterval)
            {
                coinHPManager.Damage();
                timer = 0f;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponentInParent<Player>();
            player.RequestNoDeceleration();
        }
        timer = 0f;
    }

}
