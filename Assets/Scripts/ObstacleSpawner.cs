using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private List<ObstacleData> obstacleTypes; 
    [SerializeField] private float spawnInterval = 2f; 
    [SerializeField] private float spawnZPosition = 20f;
    [SerializeField] private float despawnZPosition = -10f; 
    [SerializeField] private float laneWidth = 2f; 
    [SerializeField] private Transform playerTransform;

    private float[] lanePositions = { -2f, 0f, 2f }; 

    void Start()
    {
        if (obstacleTypes == null || obstacleTypes.Count == 0)
        {
            Debug.LogError("Нет типов препятствий! Добавь их в список.");
            return;
        }

        if (playerTransform == null)
        {
            Debug.LogError("PlayerTransform не назначен! Перетащи игрока в поле.");
            return;
        }

        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        ObstacleData selectedData = obstacleTypes[Random.Range(0, obstacleTypes.Count)];

        int randomLaneIndex = Random.Range(0, lanePositions.Length);
        float spawnX = lanePositions[randomLaneIndex];

        float spawnY = 0.5f;

        float spawnZ = playerTransform.position.z + spawnZPosition;

        Vector3 spawnPos = new Vector3(spawnX, spawnY, spawnZ);

        GameObject newObstacle = Instantiate(selectedData.prefab, spawnPos, Quaternion.identity);

        Obstacle obstacleComponent = newObstacle.GetComponent<Obstacle>();
        if (obstacleComponent == null)
        {
            obstacleComponent = newObstacle.AddComponent<Obstacle>();
        }
        obstacleComponent.Initialize(selectedData.damage, despawnZPosition);
    }
}