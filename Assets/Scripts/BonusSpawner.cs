using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private List<BonusData> bonusTypes; 
    [SerializeField] private float spawnInterval = 5f;  
    [SerializeField] private float spawnZPosition = 20f; 
    [SerializeField] private float despawnZPosition = -15f; 
    [SerializeField] private Transform playerTransform; 
    [SerializeField] private float spawnChance = 0.5f; 
    
    private float[] lanePositions = { -2f, 0f, 2f }; 
    
    void Start()
    {
        if (bonusTypes == null || bonusTypes.Count == 0)
        {
            Debug.LogWarning("Нет типов бонусов! Добавь их в список.");
            return;
        }
        
        if (playerTransform == null)
        {
            Debug.LogError("PlayerTransform не назначен!");
            return;
        }
        
        StartCoroutine(SpawnRoutine());
    }
    
    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            
            if (Random.value <= spawnChance)
            {
                SpawnBonus();
            }
        }
    }
    
    void SpawnBonus()
    {
        BonusData selectedBonus = bonusTypes[Random.Range(0, bonusTypes.Count)];
        
        int randomLane = Random.Range(0, lanePositions.Length);
        float spawnX = lanePositions[randomLane];
        float spawnY = 0.5f;
        float spawnZ = playerTransform.position.z + spawnZPosition;
        
        Vector3 spawnPos = new Vector3(spawnX, spawnY, spawnZ);
        
        GameObject newBonus = Instantiate(selectedBonus.prefab, spawnPos, Quaternion.identity);
        
        Bonus bonusComponent = newBonus.GetComponent<Bonus>();
        if (bonusComponent != null)
        {
            bonusComponent.Initialize(selectedBonus);
        }
        
        Debug.Log($"Бонус спавнится: {selectedBonus.bonusName} на X={spawnX}, Z={spawnZ}");
    }
}