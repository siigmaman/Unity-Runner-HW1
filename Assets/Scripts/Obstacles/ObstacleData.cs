using UnityEngine;

[CreateAssetMenu(fileName = "NewObstacle", menuName = "Runner/Obstacle Data")]
public class ObstacleData : ScriptableObject
{
    public string obstacleName;
    public GameObject prefab;
    public int damage = 1;
    public Color color = Color.red;
}