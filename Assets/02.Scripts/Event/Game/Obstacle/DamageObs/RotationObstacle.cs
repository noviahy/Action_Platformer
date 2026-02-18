using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
public class RotationObstacle : MonoBehaviour
{
    [SerializeField] private float lengthX;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float force;
    [SerializeField] private int clockWise;
    private int randomTime;
    private bool canRotate = false;
    private void Start()
    {
        getRandomValue();
        BoxCollider2D col = GetComponent<BoxCollider2D>();

        Vector3 scale = transform.localScale;

        scale.x = 0.4f;      // 두께 고정
        scale.y = lengthX;   // 길이 변경

        transform.localScale = scale;
    }
    private void FixedUpdate()
    {
        if (!canRotate)
            return;
        transform.Rotate(Vector3.forward * rotationSpeed * clockWise * Time.fixedDeltaTime);
    }
    private void getRandomValue()
    {
        randomTime = Random.Range(0, 6);
        StartCoroutine(RandomStart());
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Player"))
        {
            Vector2 closestPoint = GetComponent<Collider2D>().ClosestPoint(other.transform.position);

            var player = other.GetComponent<PlayerKnockbackHandler>();

            if (player != null)
                player.GetKnockbackInfo(closestPoint, force);
        }
    }
    IEnumerator RandomStart()
    {
        yield return new WaitForSeconds(randomTime);
        canRotate = true;
    }
}
