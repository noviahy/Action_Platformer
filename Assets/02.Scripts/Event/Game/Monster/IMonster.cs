using UnityEngine;

public interface IMonster : IKnockbackHandler
{
    public void Init(WaveManager waveManager) { }
}
