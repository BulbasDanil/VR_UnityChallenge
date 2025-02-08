using Meta.XR.MRUtilityKit;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpawnConfig", menuName = "Spawning/SpawnConfig")]
public class SpawnConfig : ScriptableObject
{
    public GameObject objectToSpawn;
    public FindSpawnPositions.SpawnLocation spawnLocation;
    public MRUKAnchor.SceneLabels label;
    public int amount;
}