using UnityEngine;

public class CoinHPManager : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    [SerializeField] private CoinHPCalculator calculator;
    [SerializeField] private int defaultHP;
    [SerializeField] private int needCoin;
    [SerializeField] private int coinAmount;
    [SerializeField] private int dmg;
    // GameManager에서만 호출되는 코드
    public void ResetData() { calculator._ReStart(defaultHP); }
    public void AddCoin() 
    {
        calculator._AddCoin(coinAmount);
        if (calculator.Coin >= needCoin)
        {
            ChangeCoinToHP();
        }
    }

    public void Damage() 
    {
        calculator._Damage(dmg);

        if (calculator.HP == 0)
        {
            eventManager.RequestGameOver(); // EventManager 이벤트
        }
    }

    public void ChangeCoinToHP()
    {
        calculator._SubCoin(needCoin);
        calculator._AddHP(1);
    }
}
