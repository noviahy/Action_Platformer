using UnityEngine;

public class CoinHPProgressManager : MonoBehaviour
{
    [SerializeField] private CoinHPProgressData coinHPTemplates;

    private int coin;
    private int hp;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        var saved = SaveLoadManager.LoadCoinHP();

        if (saved != null)
        {
            coin = saved.Coin;
            hp = saved.HP;
        }
        else
        {
            // Get data from SaveLoadManager
            coin = coinHPTemplates.Coin; 
            hp = coinHPTemplates.HP;
        }
    }

    // ===== CoinHPCalculator =====
    public int Coin => coin;
    public int HP => hp;

    // ===== Values modified externally =====
    public void SetCoin(int value) // CoinHPManager
    {
        coin = value;
    }
    public void SetHP(int value) // CoinHPManager
    {
        hp = value;
    }

    public void Save() // GameManager
    {
        SaveLoadManager.SaveCoinHP(new CoinHPSaveData // Save CoinHPData on Computer
        {
            Coin = coin,
            HP = hp
        });
    }
}
