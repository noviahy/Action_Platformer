using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothTime;
    [SerializeField] private float maxX;
    [SerializeField] private float maxY;
    [SerializeField] private float minX;
    [SerializeField] private float minY;

    private Vector3 velocity = Vector3.zero;
    private float defaultMinX;
    private float defaultMaxX;

    private void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPos = new Vector3(player.position.x, player.position.y, transform.position.z);

        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }

    public void SetWaveX(float minx, float maxx)
    {
        defaultMinX = minx;
        defaultMaxX = maxx;

        minX = minx;
        maxX = maxx;
    }
    public void SetDefaultX()
    {
        minX = defaultMinX;
        maxX = defaultMaxX;
    }
}
