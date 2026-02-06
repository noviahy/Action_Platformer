using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool IsGrounded { get; private set; }

    void OnTriggerEnter2D(Collider2D col)
    {
        IsGrounded = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        IsGrounded = false;
    }
}
