using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private void Awake()
    {
        // It will not be deleted when you change the scene
        DontDestroyOnLoad(gameObject);
    }
}
