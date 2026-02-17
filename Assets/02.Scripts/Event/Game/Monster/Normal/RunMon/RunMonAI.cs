using UnityEngine;

public class RunMonAI : MonoBehaviour
{
    [SerializeField] RunMonster runMonster;
    public float decisionInterval {  get; private set; }
    private void Start()
    {
        decisionInterval = 1f;
    }
    public void chooseNewDirection()
    {
        int random = Random.Range(0, 3);

        switch (random)
        {
            case 0:
                runMonster.ChnageMoveX(-1);
                break;
            case 1:
                runMonster.ChnageMoveX(0);
                break;
            case 2:
                runMonster.ChnageMoveX(1);
                break;
        }
    }
    public void chooseNewInterval()
    {
        decisionInterval = Random.Range(1, 3);
    }
}
