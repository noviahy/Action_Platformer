using UnityEngine;

public class CoinHPCalculator : MonoBehaviour
{
    public int HP { get; private set; }
    public int Coin { get; private set; }

    public void _AddCoin(int amount) { Coin += amount; }
    public void _SubCoin(int amount) { Coin -= amount; }
    public void _AddHP(int amount) { HP += amount; }
    public void _Damage(int dmg) {  HP -= dmg; }
    public void _ReStart(int defaultHP) { HP += defaultHP; }
}
// CoinHPManager에서만 호출
