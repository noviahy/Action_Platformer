using UnityEngine;

public class CoinHPManager : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    [SerializeField] private GameData gameData;
    [SerializeField] private int defaultHP;
    [SerializeField] private int needCoin;
    [SerializeField] private int coinAmount;
    [SerializeField] private int dmg;
    // GameManager에서만 호출되는 코드
    public void ResetData() { gameData._ReStart(defaultHP); }
    public void AddCoin() 
    {
        gameData._AddCoin(coinAmount);
        if (gameData.Coin >= needCoin)
        {
            ChangeCoinToHP();
        }
    }

    public void Damage() 
    {
        gameData._Damage(dmg);

        if (gameData.HP == 0)
        {
            eventManager.RequestGameOver(); // EventManager 이벤트
        }
    }

    public void ChangeCoinToHP()
    {
        gameData._SubCoin(needCoin);
        gameData._AddHP(1);
    }
}
