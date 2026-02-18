using UnityEngine;

// PlayerTrigger, Coin
public class CoinHPManager : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private CoinHPCalculator calculator;
    [SerializeField] private int defaultHP;
    [SerializeField] private int needCoin;
    [SerializeField] private int coinAmount;
    [SerializeField] private int dmg;
    private bool isGameOver = false;

    public static CoinHPManager Instance { get; private set; }
    private void Awake()
    {
        EventManager.RequestSaveData += SetCoinHP;
        eventManager.RefreshPlayingUI();
    }
    private void Start()
    {
        Instance = this;
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

        if (calculator.HP <= 0)
        {
            if (isGameOver)
                return;
            isGameOver = true;
            eventManager.RequestGameOver("You Died"); // EventManager 이벤트
            resetData();
        }
    }
    public void Dead()
    {
        calculator._Damage(calculator.HP);
        eventManager.RefreshPlayingUI();

        if (isGameOver)
            return;
        isGameOver = true;
        eventManager.RequestGameOver("You Died"); // EventManager 이벤트
        resetData();
    }
    public void ChangeGameOverState()
    {
        isGameOver = false;
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
