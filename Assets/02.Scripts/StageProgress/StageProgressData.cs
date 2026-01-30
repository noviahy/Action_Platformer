using UnityEngine;

[CreateAssetMenu]
public class StageProgressData : ScriptableObject
{
    public string StageID;

    public bool isCleared;
    public bool isOpened;
    public float BestTime;
    public bool CollectedCoin;
}
