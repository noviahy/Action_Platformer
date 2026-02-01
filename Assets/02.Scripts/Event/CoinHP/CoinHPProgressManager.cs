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
            coin = coinHPTemplates.Coin;
            hp = coinHPTemplates.HP;
        }
    }

    // ===== 조회 =====
    public int Coin => coin;
    public int HP => hp;

    // ===== 외부에서 값 변경 =====
    public void SetCoin(int value)
    {
        coin = value;
    }

    public void SetHP(int value)
    {
        hp = value;
    }

    // ===== 저장 =====
    public void Save()
    {
        SaveLoadManager.SaveCoinHP(new CoinHPSaveData
        {
            Coin = coin,
            HP = hp
        });
    }
}
