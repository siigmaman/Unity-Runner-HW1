using UnityEngine;

public class Bonus : MonoBehaviour
{
    private BonusData data;
    private float despawnZ = -15f;

    public void Initialize(BonusData bonusData)
    {
        data = bonusData;
        
        // Меняем цвет
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
            rend.material.color = data.color;
    }

    void Update()
    {
        if (transform.position.z < despawnZ)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                if (data.healthRestore > 0)
                {
                    player.Heal(data.healthRestore);
                    Debug.Log($"Бонус: +{data.healthRestore} HP");
                }
                
                if (data.speedMultiplier > 1f)
                {
                    player.ApplySpeedBoost(data.speedMultiplier, data.duration);
                    Debug.Log($"Бонус: x{data.speedMultiplier} скорости на {data.duration} сек");
                }
            }
            Destroy(gameObject);
        }
    }
}