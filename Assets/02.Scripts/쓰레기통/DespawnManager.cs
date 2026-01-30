using System.Linq;
using UnityEngine;

public class DespawnManager : MonoBehaviour
{
    public void DespawnAllObject()
    {
        var stageObjects = FindObjectsByType<MonoBehaviour>(
    FindObjectsInactive.Include,
    FindObjectsSortMode.None
)
.OfType<IStageObject>()
.ToArray();
        foreach (var stageObject in stageObjects)
        {
            stageObject.Despawn();
        }
    }
}
