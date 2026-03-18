using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private int groundCount = 0;
    public bool IsGrounded => groundCount > 0;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            groundCount++;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            groundCount--; ;
        }
    }
}
