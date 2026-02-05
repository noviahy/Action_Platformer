using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Player player;
    public void SetPlayer(Player newPlayer)
    {
        player = newPlayer;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            player.Walk(-1);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            player.Walk(1);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {

        }
        if (Input.GetKeyDown(KeyCode.Z))
        {

        }
        if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.LeftControl))
        {

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {

        }

    }
}
