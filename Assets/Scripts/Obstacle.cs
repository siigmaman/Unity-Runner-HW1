using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private int damage;
    private float despawnZ;

    public void Initialize(int obstacleDamage, float despawnZPosition)
    {
        damage = obstacleDamage;
        despawnZ = despawnZPosition;
    }

    void Update()
    {
        if (transform.position.z < despawnZ)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}