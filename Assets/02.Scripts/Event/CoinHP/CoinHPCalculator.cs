using UnityEngine;

// CoinHPManager
public class CoinHPCalculator : MonoBehaviour
{
    [SerializeField] private CoinHPProgressManager coinHPProgressManager;
    public int HP { get; private set; }
    public int Coin { get; private set; }

    private void Start()
    {
        // get Data from CoinHPProgressManager
        HP = coinHPProgressManager.HP; 
        Coin = coinHPProgressManager.Coin;
    }
    public void SetCoinHPData() // Set CoinHP data in CoinHPProgressManager
    {
        coinHPProgressManager.SetCoin(Coin);
        coinHPProgressManager.SetHP(HP);
    }

    public void _AddCoin(int amount) { Coin += amount; }
    public void _SubCoin(int amount) { Coin -= amount; }
    public void _AddHP(int amount) { HP += amount; }
    public void _Damage(int dmg) {  HP -= dmg; }
    public void _ReStart(int defaultHP) { HP += defaultHP; }
}
