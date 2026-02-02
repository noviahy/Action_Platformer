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
    public void Awake()
    {
        EventManager.RequestSaveData += SetCoinHP;
        eventManager.RefreshPlayingUI();
    }
    public void ResetData() { calculator._ReStart(defaultHP); }
    public void AddCoin() 
    {
        calculator._AddCoin(coinAmount);
        eventManager.RefreshPlayingUI();

        if (calculator.Coin >= needCoin)
        {
            ChangeCoinToHP();
        }
    }

    public void Damage() 
    {
        calculator._Damage(dmg);
        eventManager.RefreshPlayingUI();

        if (calculator.HP == 0)
        {
            eventManager.RequestGameOver("You Died"); // EventManager 이벤트
            ResetData();
        }
    }

    public void ChangeCoinToHP()
    {
        calculator._SubCoin(needCoin);
        calculator._AddHP(1);
        eventManager.RefreshPlayingUI();
    }

    public void SetCoinHP()
    {
        calculator.SetCoinHPData();
    }
}
