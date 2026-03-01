using UnityEngine;
using System.Collections;
public class LavaPool : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(CountDown());
    }
    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
