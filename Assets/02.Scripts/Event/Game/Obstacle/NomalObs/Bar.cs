using UnityEngine;
using System.Collections;
public class Bar : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void RequestActive(bool value)
    {
        StartCoroutine(SetActive(value));
    }

    IEnumerator SetActive(bool value)
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(value);
    }
}
