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
        if (value)
        {
            gameObject.SetActive(true);
        }
        else
        {
            StartCoroutine(SetActiveFalse());
        }
    }

    IEnumerator SetActiveFalse()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
