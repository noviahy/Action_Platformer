using UnityEngine;

// PlayerTrigger, Coin
public class CoinHPManager : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    [SerializeField] private CoinHPCalculator calculator;
    [SerializeField] private int defaultHP;
    [SerializeField] private int needCoin;
    [SerializeField] private int coinAmount;
    [SerializeField] private int dmg;
    private void Awake()
    {
        EventManager.RequestSaveData += SetCoinHP;
        eventManager.RefreshPlayingUI();
    }
    public void AddCoin() // Coin
    {
        calculator._AddCoin(coinAmount);
        eventManager.RefreshPlayingUI();

        if (calculator.Coin >= needCoin)
        {
            changeCoinToHP();
        }
    }
    public void Damage() // PlayerTrigger
    {
        calculator._Damage(dmg);
        eventManager.RefreshPlayingUI();

        if (calculator.HP == 0)
        {
            eventManager.RequestGameOver("You Died"); // EventManager ¿Ã∫•∆Æ
            resetData();
        }
    }
    public void SetCoinHP()
    {
        calculator.SetCoinHPData();
    }
    private void resetData() { calculator._ReStart(defaultHP); }
    private void changeCoinToHP()
    {
        calculator._SubCoin(needCoin);
        calculator._AddHP(1);
        eventManager.RefreshPlayingUI();
    }
}
