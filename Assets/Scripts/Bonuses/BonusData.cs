using UnityEngine;

[CreateAssetMenu(fileName = "NewBonus", menuName = "Runner/Bonus Data")]
public class BonusData : ScriptableObject
{
    public string bonusName;
    public GameObject prefab;
    public Color color;
    public float duration = 3f;
    public int healthRestore = 0;
    public float speedMultiplier = 1f;
}